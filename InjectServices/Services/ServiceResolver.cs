using InjectServices.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectServices.Services
{
    public class ServiceResolver
    {
        public List<KeyValuePair<Type, object>> Services { get; set; }

        public ServiceResolver ()
	    {
            this.Services = new List<KeyValuePair<Type, object>>();  
	    }

        public void AddService(Type interf, Type type)
        {
           // var instance = Activator.CreateInstance(type,true);
            KeyValuePair<Type, object> customService = new KeyValuePair<Type, object>(interf, type);
            this.Services.Add(customService);
            
        }
    }
}
