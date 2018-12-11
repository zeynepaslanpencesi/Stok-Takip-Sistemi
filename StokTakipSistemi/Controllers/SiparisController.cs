﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.DTOModels;
using StokTakipSistemi.Services;
using StokTakipSistemi.Services.Interfaces;

namespace StokTakipSistemi.Controllers
{
    public class SiparisController : Controller
    {
        private readonly ISiparisService _siparisService;
        private readonly IUrunService _urunService;
        private readonly IFaturaService _faturaService;

        public SiparisController(ISiparisService siparisService, IUrunService urunService, IFaturaService faturaService)
        {
            _siparisService = siparisService;
            _urunService = urunService;
            _faturaService = faturaService;
        }
        public async Task<IActionResult> Index()
        {
            var items = _siparisService.GetAll();
            var mappedItems = Mapper.Map<IEnumerable<SiparisSelfDTO>>(_siparisService.GetAll());
            var urunler = await _urunService.GetAllWithRelatives();

            foreach (var item in mappedItems)
            {
                var urun = await _urunService.Get(item.UrunId);
                item.Urun = urun.Adi;
                var fatura = await _faturaService.Get(item.FaturaId);
                item.Tarih = fatura.Tarih;
            }

            return View(mappedItems);
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

       /* public async Task<IActionResult> DeleteFromBill(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToDelete = await _siparisService.Get(id);

            if (itemToDelete != null)
            {
                await _siparisService.Delete(itemToDelete);
                int markaId = itemToDelete.MarkaId;
                return RedirectToAction("Düzenle", "Fatura", new { id = markaId });
            }

            return Json(false);
        }*/
    }
}