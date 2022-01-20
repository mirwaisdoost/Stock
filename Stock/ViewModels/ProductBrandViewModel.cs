using Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.ViewModels
{
    public class ProductBrandViewModel : Productbrand
    {
        public string Category { get; set; }
        public string Attributes { get; set; }
        public string Brands { get; set; }
    }
}
