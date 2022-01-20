using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stock.Models;

namespace Stock.Controllers
{
    public class DashboardController : Controller
    {
        private readonly StockContext _context;
        public DashboardController(StockContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Charts()
        {
            return View();
        }

        public IActionResult PendingProduct()
        {
            int? total = 0;
            var count = _context.PurchaseOrder.Where(p => p.Status == 0);

            foreach(var a in count)
            {
                var pd = _context.PurchaseOrderDetail.Where(p => p.PurchaseId == a.PurchaseId);
                total += pd.First().Quantity;
            }

            return View(total);
        }
    }
}