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
    public class AttributesController : Controller
    {
        private readonly StockContext _context;

        public AttributesController(StockContext context)
        {
            _context = context;
        }

        // GET: Attributes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attributes.ToListAsync());
        }

        // GET: Attributes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributes = await _context.Attributes
                .FirstOrDefaultAsync(m => m.AttributeId == id);
            if (attributes == null)
            {
                return NotFound();
            }

            return View(attributes);
        }

        // GET: Attributes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Attributes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Attributes attributes)
        {
            int id = 0;
            if (ModelState.IsValid)
            {
                if (_context.Attributes.Count() == 0)
                    id = 1;
                else
                    id = _context.Attributes.Max(p => p.AttributeId) + 1;

                Attributes atr = new Attributes
                {
                    AttributeId = id,
                    Name = attributes.Name,
                    Size=attributes.Size,
                    Color=attributes.Color
                };

                _context.Add(atr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(attributes);
        }

        // GET: Attributes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributes = await _context.Attributes.FindAsync(id);
            if (attributes == null)
            {
                return NotFound();
            }
            return View(attributes);
        }

        // POST: Attributes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttributeId,Name,Size,Color")] Attributes attributes)
        {
            if (id != attributes.AttributeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributesExists(attributes.AttributeId))
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
            return View(attributes);
        }

        //// GET: Attributes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var attributes = await _context.Attributes
        //        .FirstOrDefaultAsync(m => m.AttributeId == id);
        //    if (attributes == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(attributes);
        //}

        public async Task<IActionResult> Delete(int id)
        {
            var attributes = await _context.Attributes.FindAsync(id);
            _context.Attributes.Remove(attributes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttributesExists(int id)
        {
            return _context.Attributes.Any(e => e.AttributeId == id);
        }
    }
}
