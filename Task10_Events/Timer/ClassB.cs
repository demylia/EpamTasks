using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer
{
    public class ClassB
    {
        
        public void GetTick(Clock clock)
        {
            
            clock.Tick += (x, y) => {double t = y.timer; string obj = this.GetType().ToString(); Console.WriteLine("{0} {1}", t, obj); };

        }

    }
}
