﻿using System;
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
                var isSameAdresExists = await _adresService.IsExist(c => c.AdresText == adres.AdresText);

                if (!isSameAdresExists)
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

        
        [HttpPost]
        public IActionResult Edit(int id, Adres adres)
        {
            if (id != adres.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _adresService.Update(adres);
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

            var itemToDelete = await _adresService.Get(id);

            if (itemToDelete != null)
            {
                await _adresService.Delete(id);
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}