using System;
using System.Collections.Generic;

namespace Stock.Models
{
    public partial class Stocks
    {
        public int StockId { get; set; }
        public int? PurchaseId { get; set; }
        public int? ProductBrandId { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual Productbrand ProductBrand { get; set; }
    }
}
