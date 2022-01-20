using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public partial class Suplier
    {
        public Suplier()
        {
            Payment = new HashSet<Payment>();
            PurchaseOrder = new HashSet<PurchaseOrder>();
        }

        public int SuplierId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})[-. ]?([0-9]{3})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public double? Balance { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<PurchaseOrder> PurchaseOrder { get; set; }
    }
}
