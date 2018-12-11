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
    public class UnvanController : Controller
    {
        private readonly IUnvanService _unvanService;

        public UnvanController(IUnvanService unvanService)
        {
            _unvanService = unvanService;
        }
        public IActionResult Index()
        {
            var items = _unvanService.GetAll();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UnvanVM unvan)
        {
            if (ModelState.IsValid)
            {
                var isSameTitleExists = await _unvanService.IsExist(t => t.Adi == unvan.Adi);

                if (!isSameTitleExists)
                {
                    var mappedUnvan = Mapper.Map<Unvan>(unvan);
                    await _unvanService.Create(mappedUnvan);
                    return RedirectToAction("Index");
                }
            }

            return View();
        }
    }
}