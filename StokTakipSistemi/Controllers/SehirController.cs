using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Services.Interfaces;

namespace StokTakipSistemi.Controllers
{
    [Authorize]
    public class SehirController : Controller
    {
        private readonly ISehirService _service;

        public SehirController(ISehirService sehirService)
        {
            _service = sehirService;
        }
        public IActionResult Index()
        {
            var items = _service.GetAll();
            return View(items);
           
        }
    }
}