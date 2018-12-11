using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Services.Interfaces;

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
    }
}