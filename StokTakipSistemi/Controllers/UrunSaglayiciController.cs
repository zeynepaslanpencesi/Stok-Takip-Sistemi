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
    }
}