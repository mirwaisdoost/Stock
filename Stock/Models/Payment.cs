using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int? CustomerId { get; set; }
        public int? SuplierId { get; set; }
        [Required]
        [Display(Name ="Amount")]
        public double? Credit { get; set; }
        public double? Debit { get; set; }
        public string Description { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Suplier Suplier { get; set; }
    }
}
