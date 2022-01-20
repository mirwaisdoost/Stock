using Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.ViewModels
{
    public class PurchaseOrderDetailsViewModel:PurchaseOrderDetail
    {
        public string ProductName { get; set; }
        public int? pbId { get; set; }
    }
}
