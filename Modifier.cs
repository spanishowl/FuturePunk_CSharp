using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturePunk
{
    abstract class Modifier
    { 
        public abstract Array[] Mod { get; set;}

        public Modifier(object item, Array Mod)
        { 
            
        }
    }
}
