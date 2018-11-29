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
    public class MarkaController : Controller
    {
        private readonly IMarkaService _markaService;

        public MarkaController(IMarkaService markaService)
        {
            _markaService = markaService;
        }
        public IActionResult Index()
        {
            var items = _markaService.GetAll();
            return View(items);
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] MarkaVM marka)
        {
            if (ModelState.IsValid)
            {
                var isSameBrandExists = await _markaService.IsExist(b => b.Adi == marka.Adi);

                if (!isSameBrandExists)
                {
                    var mappedMarka = Mapper.Map<Marka>(marka);
                    await _markaService.Create(mappedMarka);
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}