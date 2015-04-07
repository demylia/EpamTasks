using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timer
{
    public class ClassA
    {
       
        public void StartTimer(Clock clock)
        {
            
            clock.TimeEnd += delegate(object x,TimerEndEventArgs y)
                { 
                    string t = "timeout";
                    var obj = this.GetType().ToString();
                    Console.WriteLine("{0} {1}", t, obj);
                };

        }
    }
}
