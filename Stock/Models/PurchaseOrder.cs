using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public partial class PurchaseOrder
    {
        public PurchaseOrder()
        {
            PurchaseOrderDetail = new HashSet<PurchaseOrderDetail>();
        }

        public int PurchaseId { get; set; }
        [Required]
        public DateTime? Date { get; set; }
        [Required]
        public int? SuplierId { get; set; }
        public int? Status { get; set; }

        public virtual Suplier Suplier { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetail { get; set; }
    }
}
