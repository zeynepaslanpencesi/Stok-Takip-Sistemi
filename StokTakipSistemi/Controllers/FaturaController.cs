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
    public class FaturaController : Controller
    {
        private readonly IFaturaService _faturaService;
        private readonly ISiparisService _siparisService;
        private readonly Helper _helper;

        public FaturaController(IFaturaService faturaService, ISiparisService siparisService, Helper helper)
        {
            _faturaService = faturaService;
            _siparisService = siparisService;
            _helper = helper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _faturaService.GetAllWithRelatives();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Providers = _helper.GetUrunSaglayiciSelectList();
            ViewBag.Products = _helper.GetUrunSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FaturaVM fatura)
        {
            if (ModelState.IsValid)
            {
                var mappedFatura = Mapper.Map<Fatura>(fatura);

                if (fatura.Siparisler == null)
                {
                    return NotFound();
                }

                await _faturaService.Create(mappedFatura);

                var mappedSiparisler = Mapper.Map<ICollection<Siparis>>(fatura.Siparisler);
                var faturaId = mappedFatura.Id;

                foreach (var item in mappedSiparisler)
                {
                    item.FaturaId = faturaId;
                    await _siparisService.Create(item);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(d => d.Errors);
            return View(fatura);
        }
    }
}