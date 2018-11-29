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
    }
}