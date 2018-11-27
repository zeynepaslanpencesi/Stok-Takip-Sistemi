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
    public class AdresController : Controller
    {
        private readonly IAdresService _adresService;

        public AdresController(IAdresService adresService)
        {
            _adresService = adresService;
        }
        public IActionResult Index()
        {
            var items = _adresService.GetAll();
            return View(items);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AdresVM adres)
        {
            if (ModelState.IsValid)
            {
                var isSameAddressExists = await _adresService.IsExist(c => c.AdresText == adres.AdresText);

                if (!isSameAddressExists)
                {
                    var mappedAdres = Mapper.Map<Adres>(adres);
                    await _adresService.Create(mappedAdres);
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

            var itemToUpdate = await _adresService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }
    }
}