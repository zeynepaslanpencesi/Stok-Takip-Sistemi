using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using StokTakipSistemi.ViewModels;

namespace StokTakipSistemi.Controllers
{
    public class FirmaController : Controller
    {
        private readonly IFirmaService _firmaService;

        public FirmaController(IFirmaService firmaService)
        {
            _firmaService = firmaService;
        }
        public IActionResult Index()
        {
            var items = _firmaService.GetAll();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FirmaVM firma)
        {
            if (ModelState.IsValid)
            {
                var isSameCompanyExists = await _firmaService.IsExist(c => c.Adi == firma.Adi);

                if (!isSameCompanyExists)
                {
                    var mappedFirma = Mapper.Map<Firma>(firma);
                    await _firmaService.Create(mappedFirma);
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

            var itemToUpdate = await _firmaService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(int id, Firma firma)
        {
            if (id != firma.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _firmaService.Update(firma);
                return RedirectToAction("Index");
            }

            return NotFound();
        }


    }
}