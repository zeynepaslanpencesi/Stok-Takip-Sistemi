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

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToUpdate = await _unvanService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(int id, Unvan unvan)
        {
            if (id != unvan.Id)
            {

                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _unvanService.Update(unvan);
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}