using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CalcMethods
{
    public static class CalculateBL
    {
      /// <summary>
        /// Evklid's method for two numbers
      /// </summary>
      /// <param name="first"></param>
      /// <param name="second"></param>
      /// <returns></returns>
        public static int CalculateNOD(int first, int second)
        {
            first = Math.Abs(first);
            second = Math.Abs(second);

            if (first == 0 ) return second;
            if (second == 0) return first;
            if (first == second) return first;
            if (first == 1 || second == 1) return 1;

            while (first != second)
            {
                if (first > second)
                    first -= second;
                else
                    second -= first;
            }
            return first;
        }
       
        /// <summary>
        /// Evklid's method for numbers more than two
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int CalculateNOD(params int[] numbers)
        {
            
                int buffer = CalculateNOD(numbers[0], numbers[1]);
                int nod = buffer;
                for (int i = 2; i < numbers.Length; i++)
                {
                    nod = CalculateNOD(buffer, numbers[i]);
                    buffer = nod;
                }
                return nod;
                        
        }
       /// <summary>
        ///  Evklid's method for two numbers plus time counter
       /// </summary>
       /// <param name="first"></param>
       /// <param name="second"></param>
       /// <param name="time"></param>
       /// <returns></returns>
        public static int CalculateNODAndTime(int first, int second, out double time)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            var nod = CalculateNOD(first, second);
            
            timer.Stop();
            time = timer.Elapsed.TotalMilliseconds;
            return nod;
        }
        /// <summary>
        /// Stain's method plus time counter
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static int CalculateNODAndTimeStain(int first, int second, out double time)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            if (first == 0 | second == 0)
            {
                time = timer.Elapsed.TotalMilliseconds;
                return 0;
            } 
            if (first == second) 
            {
                time = timer.Elapsed.TotalMilliseconds;
                return first;
            } 
            if (first == 1 || second == 1) 
            {
                time = timer.Elapsed.TotalMilliseconds;
                return 1;
            }
            if ((first % 2 == 0) && (second % 2 == 0))
            {
                time = timer.Elapsed.TotalMilliseconds;
                return 2 * CalculateNODAndTimeStain(first / 2, second / 2, out time);
            }
            if ((first % 2 == 0) && (second % 2 != 0))
            {
                time = timer.Elapsed.TotalMilliseconds;
                return CalculateNODAndTimeStain(first / 2, second, out time);
            }
            if ((first % 2 != 0) && (second % 2 == 0))
            {
                time = timer.Elapsed.TotalMilliseconds;
                return CalculateNODAndTimeStain(first, second / 2, out time);
            }

            return CalculateNODAndTimeStain(second, (int)Math.Abs(second - first), out time);

        }
    }
}
