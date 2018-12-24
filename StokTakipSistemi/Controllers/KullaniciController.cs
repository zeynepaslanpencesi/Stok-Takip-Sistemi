using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Helpers;
using StokTakipSistemi.Services.Interfaces;

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

        
    }
}