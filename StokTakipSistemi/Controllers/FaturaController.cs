using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Helpers;
using StokTakipSistemi.Services.Interfaces;

namespace StokTakipSistemi.Controllers
{
    public class FaturaController : Controller
    {
        private readonly IFaturaService _faturaService;
        private readonly ISiparisService _siparisService;
        private readonly Helper _helper;

        public FaturaController(IFaturaService faturaService, ISiparisService siparisService, Helper helper)
        {
            _faturaService = faturaService;
            _siparisService = siparisService;
            _helper = helper;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var items = await _faturaService.GetAllWithRelatives();
            return View(items);
        }
    }
}