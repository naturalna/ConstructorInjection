using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectServices.Clients
{
    public class TaxCalculator : ITax
    {
        public decimal CalculateTaxes()
        {
            return 10;
        }

        public string GetBankInfo()
        {
            return "SGE";
        }
    }
}
