using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Helpers;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using StokTakipSistemi.ViewModels;

namespace StokTakipSistemi.Controllers
{
    public class UrunController : Controller
    {
        private readonly IUrunService _urunService;
        private readonly Helper _helper;


        public UrunController(IUrunService urunService, Helper helper)
        {
            _urunService = urunService;
            _helper = helper;

        }
        public async Task<IActionResult> Index()
        {
            var items = await _urunService.GetAllWithRelatives();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.UrunTur = _helper.GetUrunTurSelectList();
            ViewBag.Marka = _helper.GetMarkaSelectLis();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UrunVM urun)
        {
            if (ModelState.IsValid)
            {
                var isSameProductExist = await _urunService.IsExist(p =>
                    p.Adi == urun.Adi &&
                    p.MarkaId == urun.MarkaId
                );

                if (!isSameProductExist)
                {
                    var mappedUrun = Mapper.Map<Urun>(urun);
                    await _urunService.Create(mappedUrun);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Name", "There is a same product in the database.");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(d => d.Errors);
            return View(urun);
        }

    }
}