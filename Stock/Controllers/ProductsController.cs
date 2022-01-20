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
    public class ProductsController : Controller
    {
        private readonly StockContext _context;

        public ProductsController(StockContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(int []CategoryId, int []AttributeId, int []BrandId, string ProductName, string []description)
        {
            int pid = 0;  // Product Id
            if (ModelState.IsValid)
            {
                if (_context.Product.Count() == 0)
                    pid = 1;
                else
                    pid = _context.Product.Max(p => p.ProductId) + 1;


                for (int i = 0; i < CategoryId.Length; i++)
                {

                    Productbrand pb = new Productbrand
                    {
                        ProductId = pid,
                        AttributeId = AttributeId[i],
                        BrandId = BrandId[i],
                        CatagoryId = CategoryId[i],
                        Description = description[i]
                    };

                    _context.Productbrand.Add(pb);
                }

                Product pr = new Product
                {
                    ProductId = pid,
                    Name = ProductName
                };
                _context.Product.Add(pr);

                _context.SaveChanges();
            }
            return Json("Saved!");
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name")] Product product)
        {
            if (id != product.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        //// GET: Products/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var product = await _context.Product
        //        .FirstOrDefaultAsync(m => m.ProductId == id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(product);
        //}

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Product.FindAsync(id);
            
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
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
    }
}
