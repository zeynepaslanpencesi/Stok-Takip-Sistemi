using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace StokTakipSistemi.Controllers
{
    public class DepartmanController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}