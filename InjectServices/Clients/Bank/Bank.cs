using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InjectServices;

namespace InjectServices.Clients
{
    public class Bank
    {
        [Injection]
        public ITax TaxCalculator { get; set; }
        
    }
}
