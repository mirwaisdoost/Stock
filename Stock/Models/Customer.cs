using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Orders>();
            Payment = new HashSet<Payment>();
        }

        public int CustomerId { get; set; }
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

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
    }
}
