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

    }
}