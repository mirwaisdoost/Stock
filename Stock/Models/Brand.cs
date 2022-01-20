using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public partial class Brand
    {
        public Brand()
        {
            Productbrand = new HashSet<Productbrand>();
        }

        public int Brandid { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Productbrand> Productbrand { get; set; }
    }
}
