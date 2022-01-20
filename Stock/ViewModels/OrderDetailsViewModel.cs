using Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.ViewModels
{
    public class OrderDetailsViewModel:OrderDetail
    {
        public string ProductName { get; set; }
    }
}
