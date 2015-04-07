using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;

namespace Timer
{
    public delegate void MyDelegate(object obj, TimerEndEventArgs e);
    public class TimerEndEventArgs : EventArgs
    {
        public double timer;
        public TimerEndEventArgs(double time)
            : base()
        {
            this.timer = time;
        }
    }
    public class Clock
    {
        double time;
        double step;

       
        public event MyDelegate TimeEnd;
        public event MyDelegate Tick;

        public double Time
        {
            get { return time; }
            set { time = value; }
        }
        public double Step
        {
            get { return step; }
            set { step = value; }
        }
        public Clock(double time, double step)
        {
            this.time = time;
            this.step = step;
        }

        protected virtual void OnTimeEnd(Object obj, TimerEndEventArgs e)
        {
            if (TimeEnd != null)
                 TimeEnd.Invoke(this, e);
   
        }
        protected virtual void OnTick(Object obj, TimerEndEventArgs e)
        {
            if (Tick != null)
                Tick.Invoke(this, e);

        }
        public void Start(double time,double step)
        {
           
            TimerEndEventArgs arg = new TimerEndEventArgs(time - step);
            for (double counter = step; counter < time; counter += step)
            {
                Thread.Sleep((int)step*100);
                arg.timer = time - counter;
                OnTick(this, arg);
            }
            /*double counter = step;
            while (counter < time)
            {
                Thread.Sleep((int)step*100);
                arg = new TimerEndEventArgs(time - counter);
                OnTick(this, arg);
                counter += step;
            }*/

            OnTimeEnd(this, arg);

        }
    }
  
}
