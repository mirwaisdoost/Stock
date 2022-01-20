using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock.Models;
using Stock.ViewModels;

namespace Stock.Controllers
{
    public class StocksController : Controller
    {
        private readonly StockContext _context;

        public StocksController(StockContext context)
        {
            _context = context;
        }

        // GET: Stocks
        public async Task<IActionResult> Index()
        {
            var p = (from item in _context.Stocks join value in _context.Productbrand on item.ProductBrandId equals value.ProductbrandId
                     join a in _context.Product on value.ProductId equals a.ProductId
                     select new{name = a.Name }).ToList();

            List<StockViewModel> lst = new List<StockViewModel>();
            StockViewModel svm;
            var Stock = _context.Stocks;


            int i = 0;
            foreach (var a in Stock)
            {
                svm = new StockViewModel
                {
                    StockId = a.StockId,
                    PurchaseId = a.PurchaseId,
                    Quantity = a.Quantity,
                    Price = a.Price,
                    ProductName = p[i].name
                };

                lst.Add(svm);
                i++;
            }
            return View(lst);
        }

        // GET: Stocks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocks = await _context.Stocks
                .Include(s => s.ProductBrand)
                .FirstOrDefaultAsync(m => m.StockId == id);
            if (stocks == null)
            {
                return NotFound();
            }

            return View(stocks);
        }

        // GET: Stocks/Create
        public IActionResult Create()
        {
            ViewData["ProductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId");
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId");
            return View();
        }

        // POST: Stocks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StockId,SuplierId,ProductBrandId,Price,Quantity")] Stocks stocks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stocks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId", stocks.ProductBrandId);
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId");
            return View(stocks);
        }

        // GET: Stocks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocks = await _context.Stocks.FindAsync(id);
            if (stocks == null)
            {
                return NotFound();
            }
            ViewData["ProductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId", stocks.ProductBrandId);
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId");
            return View(stocks);
        }

        // POST: Stocks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StockId,SuplierId,ProductBrandId,Price,Quantity")] Stocks stocks)
        {
            if (id != stocks.StockId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stocks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StocksExists(stocks.StockId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId", stocks.ProductBrandId);
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId");
            return View(stocks);
        }

        // GET: Stocks/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var stocks = await _context.Stocks
        //        .Include(s => s.ProductBrand)
        //        .FirstOrDefaultAsync(m => m.StockId == id);
        //    if (stocks == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(stocks);
        //}

        //// POST: Stocks/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var stocks = await _context.Stocks.FindAsync(id);
            var CountPurchase = _context.PurchaseOrderDetail.Count(p => p.PurchaseId==stocks.PurchaseId);

            if (CountPurchase == 1)
            {
                var purchase = _context.PurchaseOrder.Find(stocks.PurchaseId);
                _context.PurchaseOrder.Remove(purchase);
            }

            var price = stocks.Price * stocks.Quantity;
            var suplier = _context.Suplier.Where(p => p.SuplierId == _context.PurchaseOrder.Where
                            (t => t.PurchaseId == stocks.PurchaseId).First().SuplierId).First();
            suplier.Balance -= price;
            _context.Suplier.Update(suplier);

            var PurchaseOrderDetail = _context.PurchaseOrderDetail.Where(p => p.PurchaseId == stocks.PurchaseId && p.PoductBrandId == stocks.ProductBrandId);
            _context.PurchaseOrderDetail.Remove(PurchaseOrderDetail.First());

            _context.Stocks.Remove(stocks);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StocksExists(int id)
        {
            return _context.Stocks.Any(e => e.StockId == id);
        }
    }
}
