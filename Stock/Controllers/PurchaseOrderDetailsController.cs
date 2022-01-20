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
    public class PurchaseOrderDetailsController : Controller
    {
        private readonly StockContext _context;

        public PurchaseOrderDetailsController(StockContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrderDetails
        public async Task<IActionResult> Index(int id)
        {
            var p = (from item in _context.PurchaseOrderDetail
                     join value in _context.Productbrand on item.PoductBrandId equals value.ProductbrandId
                     join a in _context.Product on value.ProductId equals a.ProductId where item.PurchaseId==id
                     select new {id=item.PoductBrandId, name = a.Name }).ToList();

            List<PurchaseOrderDetailsViewModel> lst = new List<PurchaseOrderDetailsViewModel>();
            PurchaseOrderDetailsViewModel svm;
            var PurchaseOrderDetails = _context.PurchaseOrderDetail.Where(t=>t.PurchaseId==id);


            int i = 0;
            foreach (var a in PurchaseOrderDetails)
            {
                svm = new PurchaseOrderDetailsViewModel
                {
                    PurchaseId = a.PurchaseId,
                    Quantity = a.Quantity,
                    UnitPrice = a.UnitPrice,
                    pbId=p[i].id,
                    ProductName = p[i].name
                };

                lst.Add(svm);
                i++;
            }
            return View(lst);
        }

        // GET: PurchaseOrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderDetail = await _context.PurchaseOrderDetail
                .Include(p => p.PoductBrand)
                .Include(p => p.Purchase)
                .FirstOrDefaultAsync(m => m.PurchaseOrderDetailId == id);
            if (purchaseOrderDetail == null)
            {
                return NotFound();
            }

            return View(purchaseOrderDetail);
        }

        // GET: PurchaseOrderDetails/Create
        public IActionResult Create()
        {
            ViewData["PoductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId");
            ViewData["PurchaseId"] = new SelectList(_context.PurchaseOrder, "PurchaseId", "PurchaseId");
            return View();
        }

        // POST: PurchaseOrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(int[]PurchaseId, int[] ProductBrandId, int[] Quantity, float[] Price )
        {
           
            if (ModelState.IsValid)
            {
                for(int i = 0; i < PurchaseId.Length; i++)
                {                    
                    Stocks stock = new Stocks
                    {
                        PurchaseId=PurchaseId[i],
                        ProductBrandId = ProductBrandId[i],
                        Price = Price[i],
                        Quantity = Quantity[i]
                    };

                    _context.Stocks.Add(stock);
                }

                var purchaseOrder = _context.PurchaseOrder.Find(PurchaseId[0]);
                purchaseOrder.Status = 1;
                _context.PurchaseOrder.Update(purchaseOrder);

                int? quantity = 0;
                double? price = 0; 

                var GetQuantityPrice = _context.PurchaseOrderDetail.Where(p => p.PurchaseId == PurchaseId[0]);

                foreach(var a in GetQuantityPrice)
                {
                    quantity += a.Quantity;
                    price += a.UnitPrice;
                }

                double? Balance = quantity * price;

                var suplier = _context.Suplier.Find(purchaseOrder.SuplierId);
                suplier.Balance += Balance;
                _context.Suplier.Update(suplier);

                _context.SaveChanges();
            }

            return Json("The selected Purchase Order has been successfully Added to STOCK!");
        }

        // GET: PurchaseOrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrderDetail = await _context.PurchaseOrderDetail.FindAsync(id);
            if (purchaseOrderDetail == null)
            {
                return NotFound();
            }
            ViewData["PoductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId", purchaseOrderDetail.PoductBrandId);
            ViewData["PurchaseId"] = new SelectList(_context.PurchaseOrder, "PurchaseId", "PurchaseId", purchaseOrderDetail.PurchaseId);
            return View(purchaseOrderDetail);
        }

        // POST: PurchaseOrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseOrderDetailId,PurchaseId,PoductBrandId,Quantity,UnitPrice")] PurchaseOrderDetail purchaseOrderDetail)
        {
            if (id != purchaseOrderDetail.PurchaseOrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderDetailExists(purchaseOrderDetail.PurchaseOrderDetailId))
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
            ViewData["PoductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId", purchaseOrderDetail.PoductBrandId);
            ViewData["PurchaseId"] = new SelectList(_context.PurchaseOrder, "PurchaseId", "PurchaseId", purchaseOrderDetail.PurchaseId);
            return View(purchaseOrderDetail);
        }

        //// GET: PurchaseOrderDetails/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var purchaseOrderDetail = await _context.PurchaseOrderDetail
        //        .Include(p => p.PoductBrand)
        //        .Include(p => p.Purchase)
        //        .FirstOrDefaultAsync(m => m.PurchaseOrderDetailId == id);
        //    if (purchaseOrderDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(purchaseOrderDetail);
        //}

        //// POST: PurchaseOrderDetails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var purchaseOrderDetail = await _context.PurchaseOrderDetail.FindAsync(id);

            var CountPurchase = _context.PurchaseOrderDetail.Count(p => p.PurchaseId == purchaseOrderDetail.PurchaseId);

            if (CountPurchase == 1)
            {
                var purchase = _context.PurchaseOrder.Find(purchaseOrderDetail.PurchaseId);
                _context.PurchaseOrder.Remove(purchase);
            }

            _context.PurchaseOrderDetail.Remove(purchaseOrderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderDetailExists(int id)
        {
            return _context.PurchaseOrderDetail.Any(e => e.PurchaseOrderDetailId == id);
        }
    }
}
