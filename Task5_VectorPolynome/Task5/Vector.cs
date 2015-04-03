using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathObjects
{
    public class Vector
    {
        private int[] coordinate;
        /// <summary>
        /// 
        /// </summary>
        public static string name = "Vector";
        public int[] Coordinate
        {
            get { return (int[])coordinate.Clone(); }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(Resource.argNull);
                if (value.Length >= coordinate.Length)
                    coordinate = (int[])value.Clone();
                else
                    for (int i = 0; i < coordinate.Length; i++)
                        coordinate[i] = (i > value.Length - 1) ? 0 : coordinate[i] = value[i];
            }
        }
       

        public int this[int index]
        {
            get
            {
                if (index < 0 || index > coordinate.Length -1)
                    throw new ArgumentOutOfRangeException(Resource.argOutOfRange);
                return coordinate[index];
            }
            set
            {
                if (index < 0 || index > coordinate.Length - 1)
                    throw new ArgumentOutOfRangeException(Resource.argOutOfRange);
                coordinate[index] = value;
            }
        }

        public Vector(int n)
        {
            if (n < 1)
                throw new Exception(Resource.argOutOfRange);
            coordinate = new int[n];
        }
        public Vector(int[] vector)
        {
            this.coordinate = (int[])vector.Clone();
        }
        
        
        public int Size()
        {
            return coordinate.Length;
        }

        
        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1 == null || v2 == null)
                throw new ArgumentNullException(Resource.argNull);
            if (v1.Size() != v2.Size())
                throw new ArgumentNullException(Resource.defLength);
            Vector v3 = new Vector(v1.Size());
            for (int i = 0; i < v1.Size(); ++i)
            {
                v3.coordinate[i] = v1.coordinate[i] + v2.coordinate[i];
            }

            return v3;
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1 == null || v2 == null)
                throw new ArgumentNullException(Resource.argNull);
            if (v1.Size() != v2.Size())
                throw new ArgumentNullException(Resource.defLength);
            Vector v3 = new Vector(v1.Size());
            for (int i = 0; i < v1.Size(); ++i)
            {
                v3.coordinate[i] = v1.coordinate[i] - v2.coordinate[i];
            }

            return v3;
        }
        
        public static Vector operator *(Vector v1, Vector v2)
        {
            if (v1 == null || v2 == null)
                throw new ArgumentNullException(Resource.argNull);
            if (v1.Size() != v2.Size())
                throw new ArgumentNullException(Resource.defLength);
            Vector v3 = new Vector(v1.Size());
            for (int i = 0; i < v1.Size(); ++i)
            {
                v3.coordinate[i] = v1.coordinate[i] * v2.coordinate[i];
            }

            return v3;
        }

    }
}
