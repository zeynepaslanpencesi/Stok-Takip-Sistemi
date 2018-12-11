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

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Providers = _helper.GetUrunSaglayiciSelectList();
            ViewBag.Products = _helper.GetUrunSelectList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillVM bill)
        {
            if (ModelState.IsValid)
            {
                var mappedBill = Mapper.Map<Bill>(bill);

                if (bill.Orders == null)
                {
                    return NotFound();
                }

                await _billService.Create(mappedBill);

                var mappedOrders = Mapper.Map<ICollection<Order>>(bill.Orders);
                var billId = mappedBill.Id;

                foreach (var item in mappedOrders)
                {
                    item.BillId = billId;
                    await _orderService.Create(item);
                }

                return RedirectToAction("Index");
            }

            ViewBag.Errors = ModelState.Values.SelectMany(d => d.Errors);
            return View(bill);
        }
    }
}