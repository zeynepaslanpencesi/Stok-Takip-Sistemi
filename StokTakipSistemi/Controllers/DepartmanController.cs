using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using StokTakipSistemi.ViewModels;

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DepartmanVM departman)
        {
            if (ModelState.IsValid)
            {
                var isSameDepartmentExists = await _departmanService.IsExist(d => d.Adi == departman.Adi);

                if (!isSameDepartmentExists)
                {
                    var mappedDepartman = Mapper.Map<Departman>(departman);
                    await _departmanService.Create(mappedDepartman);
                    return RedirectToAction("Index");
                }
            }

            ViewBag.Errors = ModelState.Values.SelectMany(d => d.Errors);
            return View(departman);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemToUpdate = await _departmanService.Get(id);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            return View(itemToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, Departman departman)
        {
            if (id != departman.Id)
            {
                return NotFound();
            }

            ViewBag.Errors = ModelState.Values.SelectMany(e => e.Errors);

            if (ModelState.IsValid)
            {
                await _departmanService.Update(departman);
                return RedirectToAction("Index");
            }


            return View();
        }
    }
}