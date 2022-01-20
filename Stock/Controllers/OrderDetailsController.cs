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
    public class OrderDetailsController : Controller
    {
        private readonly StockContext _context;

        public OrderDetailsController(StockContext context)
        {
            _context = context;
        }


        // GET: OrderDetails
        public async Task<IActionResult> Index(int id=0)
        {

            var p = (from item in _context.OrderDetail
                     join value in _context.Productbrand on item.ProductBrandId equals value.ProductbrandId
                     join a in _context.Product on value.ProductId equals a.ProductId
                     where item.OrderId == id
                     select new { id = item.ProductBrandId, name = a.Name }).ToList();


            List<OrderDetailsViewModel> lst = new List<OrderDetailsViewModel>();
            OrderDetailsViewModel ovm;

            var OrderDetails = _context.OrderDetail.Where(t => t.OrderId == id);

            if (id == 0)
            {
                var stockContext = _context.OrderDetail.Include(o => o.Order).Include(o => o.ProductBrand);
                return View(await stockContext.ToListAsync());
            }
            else
            {
                int i = 0;
                foreach (var a in OrderDetails)
                {
                    ovm = new OrderDetailsViewModel
                    {
                        OrderDetailId=a.OrderDetailId,
                        OrderId=a.OrderId,
                        Quantity = a.Quantity,
                        UnitPrice = a.UnitPrice,
                        ProductName = p[i].name
                    };

                    lst.Add(ovm);
                    i++;
                }
                return View(lst);
            }
            
        }

        // GET: OrderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail
                .Include(o => o.Order)
                .Include(o => o.ProductBrand)
                .FirstOrDefaultAsync(m => m.OrderDetailId == id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            return View(orderDetail);
        }

        // GET: OrderDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId");
            ViewData["ProductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId");
            return View();
        }

        // POST: OrderDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailId,OrderId,ProductBrandId,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId", orderDetail.ProductBrandId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderDetail = await _context.OrderDetail.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId", orderDetail.ProductBrandId);
            return View(orderDetail);
        }

        // POST: OrderDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailId,OrderId,ProductBrandId,Quantity,UnitPrice")] OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderDetailExists(orderDetail.OrderDetailId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "OrderId", orderDetail.OrderId);
            ViewData["ProductBrandId"] = new SelectList(_context.Productbrand, "ProductbrandId", "ProductbrandId", orderDetail.ProductBrandId);
            return View(orderDetail);
        }

        // GET: OrderDetails/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var orderDetail = await _context.OrderDetail
        //        .Include(o => o.Order)
        //        .Include(o => o.ProductBrand)
        //        .FirstOrDefaultAsync(m => m.OrderDetailId == id);
        //    if (orderDetail == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(orderDetail);
        //}

        //// POST: OrderDetails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var orderDetail = await _context.OrderDetail.FindAsync(id);
            var price = orderDetail.UnitPrice*orderDetail.Quantity;
            var customer = _context.Customer.Where(p => p.CustomerId == _context.Orders.Where
                            (t => t.OrderId == orderDetail.OrderId).First().CustomerId).First();
            customer.Balance -= price;
            _context.Customer.Update(customer);

            var CountOrder = _context.OrderDetail.Count(p => p.OrderId == orderDetail.OrderId);
            if (CountOrder == 1)
            {
                var order = _context.Orders.Find(orderDetail.OrderId);
                _context.Orders.Remove(order);
            }

            _context.OrderDetail.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetail.Any(e => e.OrderDetailId == id);
        }
    }
}
