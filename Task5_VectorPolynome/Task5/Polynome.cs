using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MathObjects
{
    public class PolynomeExeption : Exception
    {
        public PolynomeExeption(string ex)
            : base(ex)
        {

        }
    }
    public class Polynome
    {
        int[] coefficient;//
        public static string name = "Polynome";
        public int this[int index]
        {
            //
            get
            {
                if (index < 0 || index > coefficient.Length - 1)
                    throw new ArgumentOutOfRangeException(Resource.argOutOfRange);

                return coefficient[index];
            }
            set
            {
                if (index < 0 || index > coefficient.Length - 1)
                    throw new ArgumentOutOfRangeException(Resource.argOutOfRange);
                coefficient[index] = value; 
            }
        }

        public Polynome(int[] coefficients)//
        {
            if (coefficients == null)//null
                throw new ArgumentNullException(Resource.argNull);//
            
            coefficient = (int[])coefficients.Clone();//clone
            
        }
        /// <summary>
        /// Calculating the polynome for some "x"
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double CalculatePolynome(double x)
        {
            if (x == 0)
                return 0;
            if (x == 1)
                return coefficient.Sum();

            double res = 0;
            for (int i = 0; i < coefficient.Length; i++)
                if (coefficient[i] != 0)
                    res += Math.Pow(x, i) * coefficient[i];
            
            return res;
        }
        /// <summary>
        /// Override method ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString() + this.coefficient.Length;
        }
        /// <summary>
        /// Overload the operator - plus
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Polynome operator +(Polynome m1, Polynome m2)
        {
            if (m1 == null || m2 == null)//null
                throw new ArgumentNullException(Resource.argNull);//
            Polynome m3 = new Polynome(new int[m1.coefficient.Length]);

            for (int i = 0; i < m1.coefficient.Length; i++)
                m3[i] = m1[i] + m2[i];
            
            return m3;
        }
        /// <summary>
        /// Overload the operator - minus
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static Polynome operator -(Polynome m1, Polynome m2)
        {
            if (m1 == null || m2 == null)//null
                throw new ArgumentNullException(Resource.argNull);//
            Polynome m3 = new Polynome(new int[m1.coefficient.Length]);

            for (int i = 0; i < m1.coefficient.Length; i++)
                 m3[i] = m1[i] - m2[i];
         
            return m3;
        }
        /// <summary>
        /// Overload the operator - "=="
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator ==(Polynome m1, Polynome m2)
        {
            if (Equals(m1, null) && Equals(m2, null))
                return true;
            if (Equals(m1, null) || Equals(m2, null))
                return false;

            return (m1.Equals(m2));
        }
        public override bool Equals(Object obj)
        {
            if (obj == null) 
                return false;

            Polynome m2 = obj as Polynome;

            if (m2 == null)
                throw new PolynomeExeption(Resource.argNull);

            for (int i = 0; i < m2.coefficient.Length-1; i++)
                if (this[i] != m2[i])
                    return false;

            return true;
        }
        /// <summary>
        /// Overload the operator - "!="
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator !=(Polynome m1, Polynome m2)
        {
            if (m1 == null || m2 == null)//null
                throw new ArgumentNullException(Resource.argNull);//

           return !(m1 == m2);
        }
       
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
        /// <summary>
        /// Overload the operator - ">","<"
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator >(Polynome m1, Polynome m2)//
        {
            if (m1 == null || m2 == null)//null
                throw new ArgumentNullException(Resource.argNull);//

            return (m1.CalculatePolynome(1) > m2.CalculatePolynome(1));
        }
        public static bool operator <(Polynome m1, Polynome m2)//
        {
            if (m1 == null || m2 == null)//null
                throw new ArgumentNullException(Resource.argNull);//

            return (m1.CalculatePolynome(1) < m2.CalculatePolynome(1));
        }
        
    }
}
