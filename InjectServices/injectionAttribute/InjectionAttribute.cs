using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InjectServices
{
    [AttributeUsage(AttributeTargets.Property)]
    public class InjectionAttribute : Attribute
    {
        public InjectionAttribute()
        {
        }
    }
}
