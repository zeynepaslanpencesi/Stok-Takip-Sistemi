using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Services.Interfaces;

namespace StokTakipSistemi.Controllers
{
    public class UrunController : Controller
    {
        private readonly IUrunService _urunService;
        

        public UrunController(IUrunService urunService)
        {
            _urunService = urunService;
           
        }
        public IActionResult Index()
        {
            var items = _urunService.GetAll();
            return View(items);

        }
    }
}