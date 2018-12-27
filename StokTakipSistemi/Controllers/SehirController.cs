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
    
    public class SehirController : Controller
    {
        private readonly ISehirService _service;
        

        public SehirController(ISehirService sehirService)
        {
            _service = sehirService;
        }
        public IActionResult Index()
        {
            var items = _service.GetAll();
            return View(items);
           
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SehirVM sehir)
        {
            if (ModelState.IsValid)
            {
                var isSameSehirExists = await _service.IsExist(c => c.Adi == sehir.Adi);

                if (!isSameSehirExists)
                {
                    var mappedSehir = Mapper.Map<Sehir>(sehir);
                    await _service.Create(mappedSehir);
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
            var itemToUpdate = await _service.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(int id, Sehir sehir)
        {
            if (id != sehir.Id)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _service.Update(sehir);
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

            var itemToDelete = await _service.Get(id);

            if (itemToDelete != null)
            {
                await _service.Delete(id);
                return RedirectToAction("Index");
            }

            return NotFound();
        }

    }
}