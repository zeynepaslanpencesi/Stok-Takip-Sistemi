using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Services.Interfaces;

namespace StokTakipSistemi.Controllers
{
    public class DepartmanController : Controller
    {
        private readonly IDepartmanService _departmanService;
        public DepartmanController(IDepartmanService departmanService)
        {
            _departmanService = departmanService;
        }
        public IActionResult Index()
        {
            var items = _departmanService.GetAll();
            return View(items);
        }
    }
}