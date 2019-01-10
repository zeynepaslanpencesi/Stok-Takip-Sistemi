using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.DTOModels;
using StokTakipSistemi.Helpers;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services;
using StokTakipSistemi.Services.Interfaces;
using StokTakipSistemi.ViewModels;

namespace StokTakipSistemi.Controllers
{
    [Authorize]
    public class SiparisController : Controller
    {
        private readonly ISiparisService _siparisService;
        private readonly IUrunService _urunService;
        private readonly IFaturaService _faturaService;
        private readonly Helper _helper;

        public SiparisController(ISiparisService siparisService, IUrunService urunService, IFaturaService faturaService, Helper helper)
        {
            _siparisService = siparisService;
            _urunService = urunService;
            _faturaService = faturaService;
            _helper = helper;
        }
        public async Task<IActionResult> Index()
        {
            var items = _siparisService.GetAll();
            var mappedItems = Mapper.Map<IEnumerable<SiparisSelfDTO>>(_siparisService.GetAll());
            var products = await _urunService.GetAllWithRelatives();

            foreach (var item in mappedItems)
            {
                var product = await _urunService.Get(item.UrunId);
                //item.Urun = product.Adi;
                var bill = await _faturaService.Get(item.FaturaId);
                //item.Tarih = bill.Tarih;
            }

            return View(mappedItems);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Urun = _helper.GetUrunSelectList();           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Siparis siparis)
        {
            if (ModelState.IsValid)
            {
                var isSameProductExist = await _siparisService.IsExist(p =>
                    p.Adet == siparis.Adet &&
                    p.UrunId == siparis.UrunId
                );

                if (!isSameProductExist)
                {
                    var mappedSiparis = Mapper.Map<Siparis>(siparis);
                    await _siparisService.Create(mappedSiparis);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Name", "There is a same product in the database.");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(d => d.Errors);
            return View(siparis);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToUpdate = await _siparisService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public IActionResult Edit(int id, Siparis siparis)
        {
            if (id != siparis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _siparisService.Update(siparis);
                return RedirectToAction("Index");
            }

            return NotFound();
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToDelete = await _siparisService.Get(id);

            if (itemToDelete != null)
            {
                await _siparisService.Delete(itemToDelete);
                return RedirectToAction("Index");
            }

            return NotFound();
        }

       public async Task<IActionResult> DeleteFromFatura(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToDelete = await _siparisService.Get(id);

            if (itemToDelete != null)
            {
                await _siparisService.Delete(itemToDelete);
                // markaId = itemToDelete.MarkaId;
                //return RedirectToAction("Düzenle", "Fatura", new { id = markaId });
            }

            return Json(false);
        }
    }
}