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
    public class IlceController : Controller
    {
        private readonly IIlceService _ilceService;

        public IlceController(IIlceService ilceService)
        {
            _ilceService = ilceService;
        }
        public IActionResult Index()
        {
            var items = _ilceService.GetAll();
            return View(items);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] IlceVM ilce)
        {
            if (ModelState.IsValid)
            {
                var isSameSehirExists = await _ilceService.IsExist(c => c.Adi == ilce.Adi);

                if (!isSameSehirExists)
                {
                    var mappedIlce = Mapper.Map<Ilce>(ilce);
                    await _ilceService.Create(mappedIlce);
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
            var itemToUpdate = await _ilceService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(int id, Ilce ilce)
        {
            if (id != ilce.Id)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _ilceService.Update(ilce);
                return RedirectToAction("Index");
            }

            return NotFound();
        }

    }
}