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
    public class UrunSaglayiciController : Controller
    {
        private readonly IUrunSaglayiciService _urunSaglayiciService;

        public UrunSaglayiciController(IUrunSaglayiciService urunSaglayiciService)
        {
            _urunSaglayiciService = urunSaglayiciService;
        }
        public IActionResult Index()
        {
            var items = _urunSaglayiciService.GetAll();
            return View(items);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UrunSaglayiciVM urunSaglayici)
        {
            if (ModelState.IsValid)
            {
                var isSameUrunSaglayiciExists = await _urunSaglayiciService.IsExist(p => p.Adi == urunSaglayici.Adi);

                if (!isSameUrunSaglayiciExists)
                {
                    var mappedUrunSaglayici = Mapper.Map<UrunSaglayici>(urunSaglayici);
                    await _urunSaglayiciService.Create(mappedUrunSaglayici);
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToUpdate = await _urunSaglayiciService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(int? id, UrunSaglayici urunSaglayici)
        {
            if (id != urunSaglayici.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _urunSaglayiciService.Update(urunSaglayici);
                return RedirectToAction("Index");
            }

            return NotFound();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToDelete = await _urunSaglayiciService.Get(id);

            if (itemToDelete != null)
            {
                await _urunSaglayiciService.Delete(id);
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}