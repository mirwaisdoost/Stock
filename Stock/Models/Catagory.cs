using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public partial class Catagory
    {
        public Catagory()
        {
            Productbrand = new HashSet<Productbrand>();
        }

        public int CatagoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Productbrand> Productbrand { get; set; }
    }
}
