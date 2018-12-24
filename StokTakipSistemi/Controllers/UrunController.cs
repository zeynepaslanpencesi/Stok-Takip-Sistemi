using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StokTakipSistemi.Data;
using StokTakipSistemi.Helpers;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using StokTakipSistemi.ViewModels;

namespace StokTakipSistemi.Controllers
{
    public class UrunController : Controller
    {
        private readonly IUrunService _urunService;
        private readonly StokTakipSistemiDbContext _dbContext;
        private readonly Helper _helper;

        public UrunController(IUrunService urunService, StokTakipSistemiDbContext dbContext, Helper helper)
        {
            _urunService = urunService;
            _dbContext = dbContext;
            _helper = helper;

        }       

      
        public async Task<IActionResult> Index()
        {
            var items = await _urunService.GetAllWithRelatives();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.UrunTur = _helper.GetUrunTurSelectList();
            ViewBag.Marka =   _helper.GetMarkaSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UrunVM urun)
        {
            if (ModelState.IsValid)
            {
                var isSameProductExist = await _urunService.IsExist(p =>
                    p.Adi == urun.Adi &&
                    p.MarkaId == urun.MarkaId
                );

                if (!isSameProductExist)
                {
                    var mappedUrun = Mapper.Map<Urun>(urun);
                    await _urunService.Create(mappedUrun);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Name", "There is a same product in the database.");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(d => d.Errors);
            return View(urun);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToUpdate = await _urunService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            ViewBag.UrunTurs = _helper.GetUrunTurSelectList(itemToUpdate.UrunTurId);
            ViewBag.Markas = _helper.GetMarkaSelectList(itemToUpdate.MarkaId);

            return View(itemToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Urun urun)
        {
            if (id != urun.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _urunService.Update(urun);
                return RedirectToAction("Index");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors);
            return View(urun);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToDelete = await _urunService.Get(id);

            if (itemToDelete != null)
            {
                await _urunService.Delete(id);
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}