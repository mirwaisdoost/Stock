using System;
using System.Collections.Generic;

namespace Stock.Models
{
    public partial class Productbrand
    {
        public Productbrand()
        {
            OrderDetail = new HashSet<OrderDetail>();
            PurchaseOrderDetail = new HashSet<PurchaseOrderDetail>();
            Stocks = new HashSet<Stocks>();
        }

        public int ProductbrandId { get; set; }
        public int? ProductId { get; set; }
        public int? BrandId { get; set; }
        public int? CatagoryId { get; set; }
        public int? AttributeId { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        public virtual Attributes Attribute { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Catagory Catagory { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
        public virtual ICollection<Stocks> Stocks { get; set; }
    }
}
