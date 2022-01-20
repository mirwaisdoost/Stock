using System;
using System.Collections.Generic;

namespace Stock.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? OrderId { get; set; }
        public int? ProductBrandId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }

        public virtual Orders Order { get; set; }
        public virtual Productbrand ProductBrand { get; set; }
    }
}
