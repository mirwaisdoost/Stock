using System;
using System.Collections.Generic;

namespace Stock.Models
{
    public partial class Product
    {
        public Product()
        {
            Productbrand = new HashSet<Productbrand>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Productbrand> Productbrand { get; set; }
    }
}
