using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixSpace
{
    public class MatrixException : Exception
    {
        public MatrixException(string ex)
            : base(ex)
        {

        }
    }
    public class Matrix : IComparable
    {
        int n;
        int rows, columns;
        int[,] matrix;

        /// <summary>
        /// Indexator
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public int this[int i, int j]
        {
            get
            {
                if (i < 0 || j < 0 || i > matrix.Length - 1 || j > matrix.Length - 1)
                    throw new MatrixException(Resource.Size);
                return matrix[i, j];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(Resource.ArgNull);

                if (i < 0 || j < 0 || i > matrix.Length - 1 || j > matrix.Length - 1)
                    throw new MatrixException(Resource.Size);
                matrix[i, j] = value;
            }
        }
        /// <summary>
        /// Constructors
        /// </summary>
        /// <param name="n"></param>
        public Matrix(int n)
        {
            if (n <= 0)
                throw new MatrixException(Resource.Size);
            this.n = n;
            this.rows = n;
            this.columns = n;
            matrix = new int[n, n];

        }
        public Matrix(int[,] matrix)
        {
            if (matrix == null)
                throw new MatrixException(Resource.ArgNull);
            
            this.matrix = (int[,])matrix.Clone();
        }
        public Matrix(int rows, int columns)
        {
            if (rows < 1 || columns < 1)
                throw new MatrixException(Resource.Size);
            this.rows = n;
            this.columns = n;
            matrix = new int[rows, columns];
        }
        public void Create()
        {
            Random rnd = new Random();
            for (int i = 0; i < n; i++)
                 for (int j = 0; j < n; j++)
                     matrix[i, j] = rnd.Next(1, 10) + (int)Math.Round(rnd.NextDouble(), 2);
        
        }

        /// <summary>
        /// Reload of  operator +
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator +(Matrix A, Matrix B)
        {
            if (A == null && B == null)
                throw new MatrixException(Resource.ArgNull);
            if (A.rows != B.rows && A.columns != B.columns)
                throw new MatrixException(Resource.MatrixsNotEqual);
           
            Matrix C = new Matrix(A.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    C.matrix[i, j] = A.matrix[i, j] + B.matrix[i, j];
                }
            }
            return C;
        }
        /// <summary>
        /// Reload of  operator -
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator -(Matrix A, Matrix B)
        {
            if (A == null && B == null)
                throw new MatrixException(Resource.ArgNull);
            if (A.rows != B.rows && A.columns != B.columns)
                throw new MatrixException(Resource.MatrixsNotEqual);
           
            Matrix D = new Matrix(A.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    D.matrix[i, j] = A.matrix[i, j] - B.matrix[i, j];
                }
            }
            return D;
        }
        /// <summary>
        /// Reload of  operator *
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static Matrix operator *(Matrix A, Matrix B)
        {
            if (A == null && B == null)
                throw new MatrixException(Resource.ArgNull);
            if (A.columns != B.rows)
                throw new MatrixException(Resource.Size);
           
            Matrix C = new Matrix(A.n);
            for (int i = 0; i < A.n; i++)
            {
                for (int j = 0; j < A.n; j++)
                {
                    C.matrix[i, j] = 0;
                    for (int k = 0; k < A.n; k++)
                    {
                        C.matrix[i, j] += A.matrix[k, j] * B.matrix[i, k];
                    }
                }
            }
            return C;
        }
        /// <summary>
        /// Invers matrix
        /// </summary>
        /// <returns></returns>
        public Matrix Inverse()
        {
            Matrix C = new Matrix(new int[this.rows, this.rows]);
            for (int row = 0; row < this.rows; row++)
            {
                for (int column = 0; column < this.columns; column++)
                    C[row, column] = matrix[column, row];
            }
            return C;
        }
        /// <summary>
        /// Interface "CompareTo"
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            Matrix matrix = obj as Matrix;
            if (obj == null)
                throw new MatrixException(Resource.ArgNull);
            if (this.rows == matrix.rows && this.columns == matrix.columns)
                return 0;
            if (this.rows * this.columns < matrix.rows * matrix.columns)
                return 1;

            return -1;
        }

    }

}