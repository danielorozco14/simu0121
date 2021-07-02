using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF3D
{
    using Vector = List<float>;
    using Matrix = List<List<float>>;
    class Math_tools
    {
        
        public static void zeroes(Matrix M, int n)
        {
            for (int i =0; i < n; i++)
            {
                List<float> row = new List<float>();
                for(int j = 0; j < n; j++)
                {
                    row.Add(0.0f);
                }
                M.Add(row);
            }
        }

        public static void zeroes(Matrix M, int n, int m)
        {
            for (int i = 0; i < n; i++)
            {
                List<float> row = new List<float>();
                for (int j = 0; j < m; j++)
                {
                    row.Add(0.0f);
                }
                M.Add(row);
            }
        }
        
        public static void zeroes(Vector v, int n)
        {
            for(int i =0; i < n; i++)
            {
                v.Add(0.0f);
            }
        }

        public static void copyMatrix(Matrix A, Matrix copy)
        {
            zeroes(copy, A.Count);

            for(int i =0; i < A.Count; i++)
            {
                for(int j =0; j < A.ElementAt(0).Count; j++)
                {
                    copy[i][j] = A[i][j];
                }
            }
        }

        public static float calculateMember(int i, int j, int r, Matrix A, Matrix B)
        {
            float member = 0f;

            for (int k = 0; k < r; k++)
                member += A.ElementAt(i).ElementAt(k) * B.ElementAt(k).ElementAt(j);

            return member;
        }

        public static Matrix productMatrixMatrix(Matrix A, Matrix B, int n, int r, int m)
        {
            Matrix R = new Matrix();
            zeroes(R, n, m);
            
            for(int i =0; i<n; i++)
            {
                for(int j =0; j < m; j++)
                {
                    R[i][j] = calculateMember(i, j, r, A, B);
                }
            }
            return R;
        }

        public static void productMatrixVector(Matrix A, Vector v, Vector result)
        {
            for(int f = 0; f < A.Count; f++)
            {
                float cell = 0.0f;
                for(int c = 0; c < v.Count; c++)
                {
                    cell += A[f][c] * v[c];
                }

                result[f] += cell;
            }
        }

        public static void productRealMatrix(float real, Matrix M, Matrix R)
        {
            zeroes(R, M.Count);

            for(int i =0; i < M.Count; i++)
            {
                for(int j =0; j < M.ElementAt(0).Count; j++)
                {
                    R[i][j] = real * M[i][j];
                }
            }
        }
        
        public static void getMinor(Matrix M, int i, int j)
        {
            M.RemoveAt(i);
            for(int k =0; k < M.Count; k++)
            {
                M.ElementAt(k).RemoveAt(j);
            }
        }

        public static float determinant(Matrix M)
        {
            if (M.Count == 1) return M[0][0];

            else
            {
                float det = 0.0f;

                for(int i =0; i < M[0].Count; i++)
                {
                    Matrix minor = new Matrix();
                    copyMatrix(M, minor);
                    getMinor(minor, 0, i);
                    det += (float) Math.Pow(-1, i) * M[0][i] * determinant(minor);
                }
                return det;
            }
        }
        
        public static void cofactors(Matrix M, Matrix Cof)
        {
            zeroes(Cof, M.Count);

            for(int i =0; i < M.Count; i++)
            {
                for(int j =0; j < M[0].Count; j++)
                {
                    Matrix minor = new Matrix();
                    copyMatrix(M, minor);
                    getMinor(minor, i, j);
                    Cof[i][j] = (float) Math.Pow(-1, i + j) * determinant(minor);
                }
            }
        }

        public static void transpose(Matrix M, Matrix T)
        {
            zeroes(T, M[0].Count, M.Count);
            for(int i =0; i < M.Count; i++)
            {
                for(int j =0; j < M[0].Count; j++)
                {
                    T[j][i] = M[i][j];
                }
            }
        }

        public static void inverseMatrix(Matrix M, Matrix Minv)
        {
            Console.WriteLine("Iniciando calculo de inversa...");
            Matrix Cof = new Matrix();
            Matrix Adj = new Matrix();
            Console.WriteLine("Calclo de determinante...");
            float det = determinant(M);
            if (det == 0)
                Environment.Exit(1);
            
            Console.WriteLine("Iniciando calculo de cofactores...");
            cofactors(M, Cof);
            
            Console.WriteLine("Calculo de adjunta...");
            transpose(Cof, Adj);
            
            Console.WriteLine("Calculo de inversa");
            productRealMatrix(1 / det, Adj, Minv);


        }

    }
}
