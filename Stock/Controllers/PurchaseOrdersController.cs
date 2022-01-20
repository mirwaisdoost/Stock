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
    public class PurchaseOrdersController : Controller
    {
        private readonly StockContext _context;

        public PurchaseOrdersController(StockContext context)
        {
            _context = context;
        }

        // GET: PurchaseOrders
        public async Task<IActionResult> Index()
        {
            var stockContext = _context.PurchaseOrder.Include(p => p.Suplier);
            return View(await stockContext.Where(p=>p.Status==0).ToListAsync());
        }

        // GET: PurchaseOrders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder
                .Include(p => p.Suplier)
                .FirstOrDefaultAsync(m => m.PurchaseId == id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }

            return View(purchaseOrder);
        }

        // GET: PurchaseOrders/Create
        public IActionResult Create()
        {
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId");
            return View();
        }

        // POST: PurchaseOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(DateTime Pdate, int SuplierId, int[] ProductBrandId, int[] Quantity, float []Price)
        {
            int poid = 0;  // Purchase Order Id
            if (ModelState.IsValid)
            {
                if (_context.PurchaseOrder.Count() == 0)
                    poid = 1;
                else
                    poid = _context.PurchaseOrder.Max(p => p.PurchaseId) + 1;


                for (int i = 0; i < ProductBrandId.Length; i++)
                {

                    PurchaseOrderDetail pod = new PurchaseOrderDetail
                    {
                        PurchaseId=poid,
                        PoductBrandId=ProductBrandId[i],
                        Quantity=Quantity[i],
                        UnitPrice=Price[i]
                    };

                    _context.PurchaseOrderDetail.Add(pod);
                }

                PurchaseOrder pr = new PurchaseOrder
                {
                    PurchaseId = poid,
                    Date=Pdate,
                    SuplierId = SuplierId,
                    Status=0
                };
                _context.PurchaseOrder.Add(pr);

                _context.SaveChanges();
            }
            return Json("Saved!");
        }

        // GET: PurchaseOrders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseOrder = await _context.PurchaseOrder.FindAsync(id);
            if (purchaseOrder == null)
            {
                return NotFound();
            }
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId", purchaseOrder.SuplierId);
            return View(purchaseOrder);
        }

        // POST: PurchaseOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PurchaseId,Date,SuplierId")] PurchaseOrder purchaseOrder)
        {
            if (id != purchaseOrder.PurchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseOrder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseOrderExists(purchaseOrder.PurchaseId))
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
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId", purchaseOrder.SuplierId);
            return View(purchaseOrder);
        }

        public IActionResult SearchProduct(int ProductId)
        {
            var pb = _context.Productbrand.Where(p => p.ProductId == ProductId).ToList();

            //List<int> pbid = new List<int>();
            //List<string> category = new List<string>();
            //List<string> attribute = new List<string>();
            //List<string> brand = new List<string>();

            List<ProductBrandViewModel> pbvm = new List<ProductBrandViewModel>();

            foreach(var x in pb)
            {
                //pbid.Add(x.ProductbrandId);
                var c = _context.Catagory.Where(p => p.CatagoryId == x.CatagoryId).ToList();
                var a = _context.Attributes.Where(p => p.AttributeId == x.AttributeId).ToList();
                var b = _context.Brand.Where(p => p.Brandid == x.BrandId).ToList();
                //category.Add(c.First().Name);
                //attribute.Add(a.First().Name);
                //brand.Add(b.First().Name);


                ProductBrandViewModel pbm = new ProductBrandViewModel
                {
                    ProductbrandId = x.ProductbrandId,
                    Category=c.First().Name,
                    Attributes=a.First().Name,
                    Brands=b.First().Name
                };

                pbvm.Add(pbm);
            }


            return new JsonResult(pbvm);
        }

        //// GET: PurchaseOrders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var purchaseOrder = await _context.PurchaseOrder
        //        .Include(p => p.Suplier)
        //        .FirstOrDefaultAsync(m => m.PurchaseId == id);
        //    if (purchaseOrder == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(purchaseOrder);
        //}

        //// POST: PurchaseOrders/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var purchaseOrder = await _context.PurchaseOrder.FindAsync(id);
            _context.PurchaseOrder.Remove(purchaseOrder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseOrderExists(int id)
        {
            return _context.PurchaseOrder.Any(e => e.PurchaseId == id);
        }


        public JsonResult AutoCompleteSuplierSelect(string term)
        {
            var suplier = (from item in _context.Suplier.Where(p => p.Name.Contains(term))
                           select new
                           { label = item.Name, id = item.SuplierId }).ToList();
            return Json(suplier);
        }

        public JsonResult AutoCompleteCategorySelect(string term)
        {

            var Category = (from item in _context.Catagory.Where(p => p.Name.Contains(term))
                            select new
                            { label = item.Name, id = item.CatagoryId }).ToList();
            return Json(Category);
        }

        public JsonResult AutoCompleteAttributeSelect(string term)
        {

            var Attribute = (from item in _context.Attributes.Where(p => p.Name.Contains(term))
                             select new
                             { label = item.Name, id = item.AttributeId }).ToList();
            return Json(Attribute);
        }

        public JsonResult AutoCompleteBrandSelect(string term)
        {

            var brand = (from item in _context.Brand.Where(p => p.Name.Contains(term))
                         select new
                         { label = item.Name, id = item.Brandid }).ToList();
            return Json(brand);
        }
        public JsonResult AutoCompleteProductSelect(string term)
        {

            var brand = (from item in _context.Product where item.Name.Contains(term)
                         select new { label=item.Name, id=item.ProductId}).ToList();
            return Json(brand);
        }

    }
}
