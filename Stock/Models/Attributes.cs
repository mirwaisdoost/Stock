using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Stock.Models
{
    public partial class Attributes
    {
        public Attributes()
        {
            Productbrand = new HashSet<Productbrand>();
        }

        public int AttributeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Size { get; set; }
        [Required]
        public string Color { get; set; }

        public virtual ICollection<Productbrand> Productbrand { get; set; }
    }
}
