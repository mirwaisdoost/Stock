using System;
using System.Collections.Generic;

namespace Stock.Models
{
    public partial class PurchaseOrderDetail
    {
        public int PurchaseOrderDetailId { get; set; }
        public int? PurchaseId { get; set; }
        public int? PoductBrandId { get; set; }
        public int? Quantity { get; set; }
        public double? UnitPrice { get; set; }

        public virtual Productbrand PoductBrand { get; set; }
        public virtual PurchaseOrder Purchase { get; set; }
    }
}
