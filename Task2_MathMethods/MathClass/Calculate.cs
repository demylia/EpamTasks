using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathClass
{
   public static class Calculate
    {
       /// <summary>
       /// Calculate the root of the number
       /// </summary>
       /// <param name="number"></param>
       /// <param name="degree"></param>
       /// <param name="eps"></param>
       /// <returns></returns>
        public static double CalculateRoot(double number, int degree, double eps = 0.001)
        {
            //Comment for a checking the github
            if (degree < 1)
                throw new Exception("The degree can't be less than 2");

            if (number < 0 && degree % 2 == 0)
                throw new Exception("Can not be removed even degree root f a negative number");

            if (degree == 1) return number;
            if (number == 1 || number == 0) return 1;
            
            double x0;
            double x = 1;
            var n = Math.Abs(number);

            do
            {
                x0 = x;
                x = (1 / degree) * ((degree - 1) * x + n / Math.Pow(x, (degree - 1)));
            }
            while (Math.Abs(x0 - x) > eps);

            return x = (number < 0 && degree % 2 == 1) ? x *= -1 : x;
        }
       /// <summary>
       /// Convert the number to binary representation
       /// </summary>
       /// <param name="number"></param>
       /// <returns></returns>
        public static string ConvertToBinary(long number)
        {
            if (number < 0) 
                number = Math.Abs(number);

            StringBuilder stringBuilder = new StringBuilder();

            while (number > 0)
            {
                stringBuilder.Insert(0, (number % 2));
                number = number / 2;
            }

            return stringBuilder.ToString();
        }
       /// <summary>
       /// Defference in accuracy between Math.Pow and CalculateRoot methods
       /// </summary>
       /// <param name="number"></param>
       /// <param name="degree"></param>
       /// <param name="def"></param>
       /// <param name="eps"></param>
       /// <returns></returns>
        public static double DefferenceMethods(double number, int degree, out double def, double eps = 0.001)
        {
            var root = CalculateRoot(number, degree, eps); 
            def = number - Math.Pow(root, degree);

            return root;
        }
    }
}
