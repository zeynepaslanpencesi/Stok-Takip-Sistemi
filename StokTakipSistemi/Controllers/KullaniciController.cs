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
    public class KullaniciController : Controller
    {
        private readonly IKullaniciService _kullaniciService;
        private readonly Helper _helper;

        public KullaniciController(IKullaniciService kullaniciService, Helper helper)
        {
            _kullaniciService = kullaniciService;
            _helper = helper;
        }
        public IActionResult Index()
        {
            var items = _kullaniciService.GetAll();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departmanlar = _helper.GetDepartmanSelectList();
            ViewBag.Unvanlar = _helper.GetUnvanSelectList();
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] KullaniciVM kullanici)
        {
            if (ModelState.IsValid)
            {
                var isSameTypeExists = await _kullaniciService.IsExist(u => u.Email == kullanici.Email);

                if (!isSameTypeExists)
                {
                    var mappedKullanici = Mapper.Map<Kullanici>(kullanici);
                    await _kullaniciService.Create(mappedKullanici);
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

            var itemToUpdate = await _kullaniciService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(int id, Kullanici kullanici)
        {
            if (id != kullanici.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _kullaniciService.Update(kullanici);
                return RedirectToAction("Index");
            }

            return NotFound();
        }


    }
}