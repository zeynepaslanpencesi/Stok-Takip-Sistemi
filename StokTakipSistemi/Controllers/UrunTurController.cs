using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Services.Interfaces;

namespace StokTakipSistemi.Controllers
{
    public class UrunTurController : Controller
    {
        private readonly IUrunTurService _urunTurService;

        public UrunTurController(IUrunTurService urunTurService)
        {
            _urunTurService = urunTurService;
        }

        public IActionResult Index()
        {
            var items = _urunTurService.GetAll();
            return View(items);
            
        }
    }
}