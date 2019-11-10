using System;
using System.Collections.Generic;
using System.Text;

namespace IoC_FromScratch
{
    public class Container
    {
        public object GetInstance(Type type)
        {
            return Activator.CreateInstance(type);
        }
    }
}
