using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExtension
{
    public static class ObjectExtension
    {
        public static object SetValueObjectExtension(this object self, object instance)
        {
            self = instance;
            return self;
        }
    }
}
