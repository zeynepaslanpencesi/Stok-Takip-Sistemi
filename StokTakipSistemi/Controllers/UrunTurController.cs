using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using StokTakipSistemi.ViewModels;

namespace StokTakipSistemi.Controllers
{
    [Authorize]
    public class UrunTurController : Controller
    {
        private readonly IUrunTurService _urunTurService;

        public UrunTurController(IUrunTurService urunTurService)
        {
            _urunTurService = urunTurService;
        }

        public IActionResult Index()
        {
            var items = _urunTurService.GetAll();
            return View(items);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UrunTurVM urunTur)
        {
            if (ModelState.IsValid)
            {
                var isSameProductTypeExist = await _urunTurService.IsExist(p => p.Adi == urunTur.Adi);

                if (!isSameProductTypeExist)
                {
                    var mappedUrunTur = Mapper.Map<UrunTur>(urunTur);
                    await _urunTurService.Create(mappedUrunTur);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Errors = ModelState.Values.SelectMany(d => d.Errors);
            return View(urunTur);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToUpdate = await _urunTurService.Get(p => p.Id == id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, UrunTur urunTur)
        {
            if (id != urunTur.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _urunTurService.Update(urunTur);
                return RedirectToAction("Index");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors);
            return View(urunTur);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToDelete = _urunTurService.Get(id);

            if (itemToDelete != null)
            {
                await _urunTurService.Delete(id);
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}