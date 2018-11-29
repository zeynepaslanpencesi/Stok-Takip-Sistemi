using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StokTakipSistemi.Data;
using StokTakipSistemi.Models;
using StokTakipSistemi.Services.Interfaces;
using StokTakipSistemi.ViewModels;

namespace StokTakipSistemi.Controllers
{
    public class UrunController : Controller
    {
        private readonly IUrunService _urunService;
        private readonly StokTakipSistemiDbContext _dbContext;

        public UrunController(IUrunService urunService, StokTakipSistemiDbContext dbContext)
        {
            _urunService = urunService;
            _dbContext = dbContext;

        }

        public IList<SelectListItem> GetUrunTurSelectList(int? id = null)
        {
            IList<SelectListItem> selectList;

            if (id == null)
            {
                selectList = _dbContext.UrunTur.ToList().
                    Select(t => new SelectListItem() { Text = t.Adi, Value = t.Id.ToString() }).
                    ToList();

                return selectList;
            }

            selectList = new List<SelectListItem>();

            foreach (var urunTur in _dbContext.UrunTur.ToList())
            {
                if (urunTur.Id == id)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = urunTur.Id.ToString(),
                        Text = urunTur.Adi,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = urunTur.Id.ToString(),
                        Text = urunTur.Adi
                    });
                }
            }
            return selectList;
        }

        public IList<SelectListItem> GetMarkaSelectList(int? id = null)
        {
            IList<SelectListItem> selectList;

            if (id == null)
            {
                selectList = _dbContext.Marka.ToList().
                    Select(t => new SelectListItem() { Text = t.Adi, Value = t.Id.ToString() }).ToList();

                return selectList;
            }

            selectList = new List<SelectListItem>();

            foreach (var marka in _dbContext.Marka.ToList())
            {
                if (marka.Id == id)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = marka.Id.ToString(),
                        Text = marka.Adi,
                        Selected = true
                    });
                }
                else
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = marka.Id.ToString(),
                        Text = marka.Adi
                    });
                }
            }
            return selectList;
        }


      
        public async Task<IActionResult> Index()
        {
            var items = await _urunService.GetAllWithRelatives();
            return View(items);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.UrunTur = GetUrunTurSelectList();
            ViewBag.Marka =   GetMarkaSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] UrunVM urun)
        {
            if (ModelState.IsValid)
            {
                var isSameProductExist = await _urunService.IsExist(p =>
                    p.Adi == urun.Adi &&
                    p.MarkaId == urun.MarkaId
                );

                if (!isSameProductExist)
                {
                    var mappedUrun = Mapper.Map<Urun>(urun);
                    await _urunService.Create(mappedUrun);
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("Name", "There is a same product in the database.");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(d => d.Errors);
            return View(urun);
        }


    }
}