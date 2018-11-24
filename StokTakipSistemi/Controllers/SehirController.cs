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
    }
}