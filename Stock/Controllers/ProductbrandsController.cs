using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stock.Models;

namespace Stock.Controllers
{
    public class ProductbrandsController : Controller
    {
        private readonly StockContext _context;

        public ProductbrandsController(StockContext context)
        {
            _context = context;
        }

        // GET: Productbrands
        public async Task<IActionResult> Index()
        {
            var stockContext = _context.Productbrand.Include(p => p.Attribute).Include(p => p.Brand).Include(p => p.Catagory).Include(p => p.Product);
            return View(await stockContext.ToListAsync());
        }

        // GET: Productbrands/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productbrand = await _context.Productbrand
                .Include(p => p.Attribute)
                .Include(p => p.Brand)
                .Include(p => p.Catagory)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.ProductbrandId == id);
            if (productbrand == null)
            {
                return NotFound();
            }

            return View(productbrand);
        }

        // GET: Productbrands/Create
        public IActionResult Create()
        {
            ViewData["AttributeId"] = new SelectList(_context.Attributes, "AttributeId", "AttributeId");
            ViewData["BrandId"] = new SelectList(_context.Brand, "Brandid", "Brandid");
            ViewData["CatagoryId"] = new SelectList(_context.Catagory, "CatagoryId", "CatagoryId");
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId");
            return View();
        }

        // POST: Productbrands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductbrandId,ProductId,BrandId,CatagoryId,AttributeId,Image,Description")] Productbrand productbrand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productbrand);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AttributeId"] = new SelectList(_context.Attributes, "AttributeId", "AttributeId", productbrand.AttributeId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "Brandid", "Brandid", productbrand.BrandId);
            ViewData["CatagoryId"] = new SelectList(_context.Catagory, "CatagoryId", "CatagoryId", productbrand.CatagoryId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productbrand.ProductId);
            return View(productbrand);
        }

        // GET: Productbrands/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productbrand = await _context.Productbrand.FindAsync(id);
            if (productbrand == null)
            {
                return NotFound();
            }
            ViewData["AttributeId"] = new SelectList(_context.Attributes, "AttributeId", "AttributeId", productbrand.AttributeId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "Brandid", "Brandid", productbrand.BrandId);
            ViewData["CatagoryId"] = new SelectList(_context.Catagory, "CatagoryId", "CatagoryId", productbrand.CatagoryId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productbrand.ProductId);
            return View(productbrand);
        }

        // POST: Productbrands/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductbrandId,ProductId,BrandId,CatagoryId,AttributeId,Image,Description")] Productbrand productbrand)
        {
            if (id != productbrand.ProductbrandId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productbrand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductbrandExists(productbrand.ProductbrandId))
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
            ViewData["AttributeId"] = new SelectList(_context.Attributes, "AttributeId", "AttributeId", productbrand.AttributeId);
            ViewData["BrandId"] = new SelectList(_context.Brand, "Brandid", "Brandid", productbrand.BrandId);
            ViewData["CatagoryId"] = new SelectList(_context.Catagory, "CatagoryId", "CatagoryId", productbrand.CatagoryId);
            ViewData["ProductId"] = new SelectList(_context.Product, "ProductId", "ProductId", productbrand.ProductId);
            return View(productbrand);
        }

        // GET: Productbrands/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var productbrand = await _context.Productbrand
        //        .Include(p => p.Attribute)
        //        .Include(p => p.Brand)
        //        .Include(p => p.Catagory)
        //        .Include(p => p.Product)
        //        .FirstOrDefaultAsync(m => m.ProductbrandId == id);
        //    if (productbrand == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(productbrand);
        //}

        public async Task<IActionResult> Delete(int id)
        {
            var productbrand = await _context.Productbrand.FindAsync(id);
            _context.Productbrand.Remove(productbrand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductbrandExists(int id)
        {
            return _context.Productbrand.Any(e => e.ProductbrandId == id);
        }
    }
}
