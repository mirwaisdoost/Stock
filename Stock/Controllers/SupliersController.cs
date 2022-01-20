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
    public class SupliersController : Controller
    {
        private readonly StockContext _context;

        public SupliersController(StockContext context)
        {
            _context = context;
        }

        // GET: Supliers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Suplier.ToListAsync());
        }

        // GET: Supliers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suplier = await _context.Suplier
                .FirstOrDefaultAsync(m => m.SuplierId == id);
            if (suplier == null)
            {
                return NotFound();
            }

            return View(suplier);
        }

        // GET: Supliers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Suplier suplier)
        {
            var id = 1;
            if (ModelState.IsValid)
            {
                if (_context.Suplier.Count() == 0)
                    id = 1;
                else
                    id = _context.Suplier.Max(p => p.SuplierId) + 1;
                Suplier sup = new Suplier
                {
                    SuplierId = id,
                    Name = suplier.Name,
                    Phone = suplier.Phone,
                    Email = suplier.Email,
                    Address = suplier.Address,
                    Balance = 0
                };

                _context.Suplier.Add(sup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(suplier);
        }

        // GET: Supliers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var suplier = await _context.Suplier.FindAsync(id);
            if (suplier == null)
            {
                return NotFound();
            }
            return View(suplier);
        }

        // POST: Supliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SuplierId,Name,Phone,Email,Address,Balance")] Suplier suplier)
        {
            if (id != suplier.SuplierId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(suplier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SuplierExists(suplier.SuplierId))
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
            return View(suplier);
        }

        //// GET: Supliers/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var suplier = await _context.Suplier
        //        .FirstOrDefaultAsync(m => m.SuplierId == id);
        //    if (suplier == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(suplier);
        //}
        public async Task<IActionResult> Delete(int id)
        {
            var suplier = await _context.Suplier.FindAsync(id);
            _context.Suplier.Remove(suplier);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SuplierExists(int id)
        {
            return _context.Suplier.Any(e => e.SuplierId == id);
        }
    }
}
