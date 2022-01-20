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
    public class OrdersController : Controller
    {
        private readonly StockContext _context;

        public OrdersController(StockContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var stockContext = _context.Orders.Include(o => o.Customer);
            return View(await stockContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

       
        public IActionResult ProductQuantity(int ProductBrandId, int Quantity)
        {
            int? OrderQuantity = 0;
            int? StockQuantity = 0;
            int? result = 0;
            var order = _context.OrderDetail.Where(p => p.ProductBrandId == ProductBrandId);

            foreach(var a in order)
            {
                OrderQuantity += a.Quantity;
            }

            var stock = _context.Stocks.Where(p => p.ProductBrandId == ProductBrandId);

            foreach (var a in stock)
            {
                StockQuantity += a.Quantity;
            }

            result = StockQuantity - OrderQuantity;

            if (result >= Quantity)
                return Json(true);
            else
                return Json(false);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public IActionResult Create(DateTime Pdate, int CustomerId, int[] ProductBrandId, int[] Quantity, float[] Price)
        {
            int oid = 0;  // Order Id
            if (ModelState.IsValid)
            {
                if (_context.Orders.Count() == 0)
                    oid = 1;
                else
                    oid = _context.Orders.Max(p => p.OrderId) + 1;

                float total = 0;

                for (int i = 0; i < ProductBrandId.Length; i++)
                {

                    OrderDetail od = new OrderDetail
                    {
                        OrderId = oid,
                        ProductBrandId = ProductBrandId[i],
                        Quantity = Quantity[i],
                        UnitPrice = Price[i]
                    };

                    total += (Price[i])*Quantity[i];
                    _context.OrderDetail.Add(od);
                }

                var cust = _context.Customer.Find(CustomerId);
                cust.Balance += total;
                _context.Customer.Update(cust);

                Orders or = new Orders
                {
                    OrderId = oid,
                    Date = Pdate,
                    CustomerId = CustomerId
                };
                _context.Orders.Add(or);

                _context.SaveChanges();
            }
            return Json("Saved!");
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Name", orders.CustomerId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,Date,CustomerId")] Orders orders)
        {
            if (id != orders.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.OrderId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", orders.CustomerId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            double? Total = 0;
            
            var orders = await _context.Orders.FindAsync(id);
            var od = _context.OrderDetail.Where(p => p.OrderId == orders.OrderId).ToList();

            foreach(var t in od)
            {
                Total += (t.Quantity) * (t.UnitPrice);
            }

            var custId = orders.CustomerId;
            var customer = _context.Customer.Find(custId);
            customer.Balance -= Total;
            _context.Customer.Update(customer);

            _context.Orders.Remove(orders);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

        public JsonResult AutoCompleteCustomerSelect(string term)
        {
            var suplier = (from item in _context.Customer.Where(p => p.Name.Contains(term))
                           select new
                           { label = item.Name, id = item.CustomerId }).ToList();
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
        public JsonResult AutoCompleteProductSelect(string term, int CategoryId, int AttributeId, int BrandId)
        {

            var brand = (from item in _context.Productbrand
                         join value in _context.Product on item.ProductId equals value.ProductId
                         where value.Name.Contains(term) && item.CatagoryId == CategoryId && item.AttributeId == AttributeId && item.BrandId == BrandId
                         select new { label = value.Name, id = item.ProductbrandId }).ToList();
            return Json(brand);
        }

    }
}
