using Stock.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.ViewModels
{
    public class StockViewModel:Stocks
    {
        public string ProductName { get; set; }
    }
}
