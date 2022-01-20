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
    public class PaymentsController : Controller
    {
        private readonly StockContext _context;

        public PaymentsController(StockContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            var stockContext = _context.Payment.Include(p => p.Customer).Include(p => p.Suplier);
            return View(await stockContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Customer)
                .Include(p => p.Suplier)
                .FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "Name");
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(int transaction, int AccountId, double Amount, string description,int AccountType)
        {
            if (ModelState.IsValid)
            {
                int id = 1;
                Payment payment = new Payment();
                if (transaction == 1)
                {
                    if (_context.Payment.Count() == 0)
                        id = 1;
                    else
                        id = _context.Payment.Max(p=>p.PaymentId) + 1;
                    if (AccountType == 1)
                    {
                        payment = new Payment
                        {
                            PaymentId = id,
                            CustomerId = AccountId,
                            Credit = Amount,
                            Debit=0.00,
                            Description = description

                        };

                        var cust = _context.Customer.Find(AccountId);
                        cust.Balance = cust.Balance - Amount;
                        _context.Customer.Update(cust);
                    }
                    else
                    {
                        payment = new Payment
                        {
                            PaymentId = id,
                            SuplierId = AccountId,
                            Credit = Amount,
                            Debit=0.00,
                            Description = description

                        };

                        var sup = _context.Suplier.Find(AccountId);
                        sup.Balance = sup.Balance + Amount;
                        _context.Suplier.Update(sup);
                    }
                }
                else
                {
                    if (_context.Payment.Count() == 0)
                        id = 1;
                    else
                        id = _context.Payment.Max(p => p.PaymentId) + 1;
                    if (AccountType == 1)
                    {
                        payment = new Payment
                        {
                            PaymentId = id,
                            CustomerId = AccountId,
                            Debit = Amount,
                            Credit=0.00,
                            Description = description

                        };


                        var cust = _context.Customer.Find(AccountId);
                        cust.Balance = cust.Balance + Amount;
                        _context.Customer.Update(cust);
                    }
                    else
                    {
                        payment = new Payment
                        {
                            PaymentId = id,
                            SuplierId = AccountId,
                            Debit = Amount,
                            Credit=0.00,
                            Description = description

                        };

                        var sup = _context.Suplier.Find(AccountId);
                        sup.Balance = sup.Balance - Amount;
                        _context.Suplier.Update(sup);
                    }
                }

                _context.Payment.Add(payment);
                _context.SaveChanges();
            }
            return Json("Saved!");
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", payment.CustomerId);
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId", payment.SuplierId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,CustomerId,SuplierId,Credit,Debit,Description")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customer, "CustomerId", "CustomerId", payment.CustomerId);
            ViewData["SuplierId"] = new SelectList(_context.Suplier, "SuplierId", "SuplierId", payment.SuplierId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    _context.Payment.Remove(_context.Payment.Find(id));
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //// POST: Payments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var payment = await _context.Payment.FindAsync(id);
            if (payment.CustomerId > 0)
            {
                if (payment.Credit > 0)
                {
                    var customer = _context.Customer.Where(p => p.CustomerId == payment.CustomerId).First();
                    customer.Balance += payment.Credit;
                    _context.Customer.Update(customer);
                }
                else if (payment.Debit > 0)
                {
                    var customer = _context.Customer.Where(p => p.CustomerId == payment.CustomerId).First();
                    customer.Balance -= payment.Debit;
                    _context.Customer.Update(customer);
                }
            }
            else if (payment.SuplierId > 0)
            {
                if (payment.Credit > 0)
                {
                    var suplier = _context.Suplier.Where(p => p.SuplierId == payment.SuplierId).First();
                    suplier.Balance -= payment.Credit;
                    _context.Suplier.Update(suplier);
                }
                else if (payment.Debit > 0)
                {
                    var suplier = _context.Suplier.Where(p => p.SuplierId == payment.SuplierId).First();
                    suplier.Balance += payment.Debit;
                    _context.Suplier.Update(suplier);
                }
            }
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }

        public JsonResult AutoCompleteCustomerAccountSelect(string term, int AccountType)
        {
            var customer = (from item in _context.Customer.Where(p => p.Name.Contains(term))
                            select new
                            { label = item.Name, id = item.CustomerId }).ToList();
            return Json(customer);
        }
        public JsonResult AutoCompleteSuplierAccountSelect(string term, int AccountType)
        {
            var suplier = (from item in _context.Suplier.Where(p => p.Name.Contains(term))
                            select new
                            { label = item.Name, id = item.SuplierId }).ToList();
            return Json(suplier);
        }

    }
}
