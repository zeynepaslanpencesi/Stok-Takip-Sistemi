using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Services.Interfaces;

namespace StokTakipSistemi.Controllers
{
    public class AdresController : Controller
    {
        private readonly IAdresService _adresService;

        public AdresController(IAdresService adresService)
        {
            _adresService = adresService;
        }
        public IActionResult Index()
        {
            var items = _adresService.GetAll();
            return View(items);
            
        }
    }
}