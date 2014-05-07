using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectServices.Clients.Shop
{
    public class Shop
    {
        public ITax TaxCalculator { get; set; }
    }
}
