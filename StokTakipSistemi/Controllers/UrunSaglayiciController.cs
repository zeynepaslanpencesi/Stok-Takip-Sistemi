using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Services.Interfaces;

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
            ;
        }
    }
}