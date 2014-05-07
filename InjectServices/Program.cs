using InjectServices.Clients;
using InjectServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectServices
{
    // When we use injection we can create new instance of a class
    //depending on what kind of implementation of this class we have, 
    //and we can easil change it to another one. 
 
    class Program
    {
        static void Main(string[] args)
        {
            AvailableServiceImplementations services = new AvailableServiceImplementations();
            ServiceResolver res = new ServiceResolver();

            res.AddService(typeof(ITax), typeof(TaxCalculator));
          
            services.Init(res);
            Bank bank = new Bank();
            var x = bank.TaxCalculator;
        }
    }
}
