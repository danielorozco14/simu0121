using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MEF3D
{
    using Vector = List<float>;
    using Matrix = List<List<float>>;

    public class Sel
    {

        public static void showMatrix(Matrix K)
        {
            for (int i = 0; i < K.ElementAt(0).Count; i++)
            {
                Console.Write("[\t");
                for (int j = 0; j < K.Count; j++)
                {
                    Console.Write(K.ElementAt(i).ElementAt(j) + "\t");
                }
                Console.Write("]\n");
            }
        }

       public void showKs(List<Matrix> Ks)
        {
            for (int i = 0; i < Ks.Count; i++)
            {
                Console.WriteLine("K del elemento " + i + 1 + ":");
                showMatrix(Ks.ElementAt(i));
                Console.WriteLine("*************************************");
            }
        }

        public void showVector(Vector b)
        {
            Console.Write("[\t");
            for (int i = 0; i < b.Count; i++)
            {
                Console.WriteLine(b.ElementAt(i) + "\t");
            }
            Console.Write("]\n");

        }

        public void showbs(List<Vector> bs)
        {
            for (int i = 0; i < bs.Count; i++)
            {
                Console.WriteLine("b del elemento " + i + 1 + ":");
                showVector(bs.ElementAt(i));
                Console.WriteLine("*************************************");
            }
        }

        public float calculateLocalD(int ind, Classes.mesh m)
        {
            float D, a, b, c, d, e, f, g, h, i;

            Classes.element el = m.getElement(ind);

            Classes.node n1 = m.getNode(el.node1 - 1);
            Classes.node n2 = m.getNode(el.node2 - 1);
            Classes.node n3 = m.getNode(el.node3 - 1);
            Classes.node n4 = m.getNode(el.node4 - 1);

            a = n2.x - n1.x; b = n2.y - n1.y; c = n2.z - n1.z;
            d = n3.x - n1.x; e = n3.y - n1.y; f = n3.z - n1.z;
            g = n4.x - n1.x; h = n4.y - n1.y; i = n4.z - n1.z;
            //Se calcula el determinante de esta matriz utilizando
            //la Regla de Sarrus.
            D = a * e * i + d * h * c + g * b * f - g * e * c - a * h * f - d * b * i;

            return D;
        }

        public float calculateLocalVolume(int ind, Classes.mesh m)
        {
            //Se utiliza la siguiente fórmula:
            //      Dados los 4 puntos vértices del tetrahedro A, B, C, D.
            //      Nos anclamos en A y calculamos los 3 vectores:
            //              V1 = B - A
            //              V2 = C - A
            //              V3 = D - A
            //      Luego el volumen es:
            //              V = (1/6)*det(  [ V1' ; V2' ; V3' ]  )

            float V, a, b, c, d, e, f, g, h, i;
            Classes.element el = m.getElement(ind);
            Classes.node n1 = m.getNode(el.node1 - 1);
            Classes.node n2 = m.getNode(el.node2 - 1);
            Classes.node n3 = m.getNode(el.node3 - 1);
            Classes.node n4 = m.getNode(el.node4 - 1);

            a = n2.x - n1.x; b = n2.y - n1.y; c = n2.z - n1.z;
            d = n3.x - n1.x; e = n3.y - n1.y; f = n3.z - n1.z;
            g = n4.x - n1.x; h = n4.y - n1.y; i = n4.z - n1.z;
            //Para el determinante se usa la Regla de Sarrus.
            V = (1.0f / 6.0f) * (a * e * i + d * h * c + g * b * f - g * e * c - a * h * f - d * b * i);

            return V;
        }

        public float ab_ij(float ai, float aj, float a1, float bi, float bj, float b1)
        {
            return (ai - a1) * (bj - b1) - (aj - a1) * (bi - b1);
        }

        public void calculateLocalA(int i, Matrix A, Classes.mesh m)
        {
            Classes.element e = m.getElement(i);
            Classes.node n1 = m.getNode(e.node1 - 1);
            Classes.node n2 = m.getNode(e.node2 - 1);
            Classes.node n3 = m.getNode(e.node3 - 1);
            Classes.node n4 = m.getNode(e.node4 - 1);

            ArrayList arrayList = new ArrayList();
            arrayList.Add(1);
            arrayList[0] = 5;


            A[0][0] = ab_ij(n3.y, n4.y, n1.y, n3.z, n4.z, n1.z);
            A[0][1] = ab_ij(n4.y, n2.y, n1.y, n4.z, n2.z, n1.z);
            A[0][2] = ab_ij(n2.y, n3.y, n1.y, n2.z, n3.z, n1.z);
            A[1][0] = ab_ij(n4.x, n3.x, n1.x, n4.z, n3.z, n1.z);
            A[1][1] = ab_ij(n2.x, n4.x, n1.x, n2.z, n4.z, n1.z);
            A[1][2] = ab_ij(n3.x, n2.x, n1.x, n3.z, n2.z, n1.z);
            A[2][0] = ab_ij(n3.x, n4.x, n1.x, n3.y, n4.y, n1.y);
            A[2][1] = ab_ij(n4.x, n2.x, n1.x, n4.y, n2.y, n1.y);
            A[2][2] = ab_ij(n2.x, n3.x, n1.x, n2.y, n3.y, n1.y);
        }

        public void calculateB(Matrix B)
        {
            B[0][0] = -1; B[0][1] = 1; B[0][2] = 0; B[0][3] = 0;
            B[1][0] = -1; B[1][1] = 0; B[1][2] = 1; B[1][3] = 0;
            B[2][0] = -1; B[2][1] = 0; B[2][2] = 0; B[2][3] = 1;
        }

        public Matrix createLocalK(int element, Classes.mesh m)
        {
            // K = (k*Ve/D^2)Bt*At*A*B := K_4x4
            float D, Ve, k = m.getParameter((int)Classes.parameter.THERMAL_CONDUCTIVITY);
            Matrix K = new Matrix();
            Matrix A = new Matrix();
            Matrix B = new Matrix();
            Matrix Bt = new Matrix();
            Matrix At = new Matrix();

            D = calculateLocalD(element, m);
            Ve = calculateLocalVolume(element, m);

            Math_tools.zeroes(A, 3);
            Math_tools.zeroes(B, 3, 4);
            calculateLocalA(element, A, m);
            calculateB(B);
            Math_tools.transpose(A, At);
            Math_tools.transpose(B, Bt);

            Math_tools.productRealMatrix(k * Ve / (D * D),
                Math_tools.productMatrixMatrix(Bt, Math_tools.productMatrixMatrix(At,Math_tools.productMatrixMatrix(A, B, 3, 3, 4), 3, 3, 4),
                4, 3, 4), K);

            return K;
        }

        public float calculateLocalJ(int ind, Classes.mesh m)
        {
            float J, a, b, c, d, e, f, g, h, i;

            Classes.element el = m.getElement(ind);

            Classes.node n1 = m.getNode(el.node1 - 1);
            Classes.node n2 = m.getNode(el.node2 - 1);
            Classes.node n3 = m.getNode(el.node3 - 1);
            Classes.node n4 = m.getNode(el.node4 - 1);

            a = n2.x - n1.x; b = n3.x - n1.x; c = n4.x - n1.x;
            d = n3.y - n1.y; e = n3.y - n1.y; f = n4.y - n1.y;
            g = n4.z - n1.z; h = n3.z - n1.z; i = n4.z - n1.z;

            //Se calcula el determinante de esta matriz utilizando
            //la Regla de Sarrus.
            J = a * e * i + d * h * c + g * b * f - g * e * c - a * h * f - d * b * i;

            return J;
        }

        public Vector createLocalb(int element, Classes.mesh m)
        {
            Vector b = new Vector();

            float Q = m.getParameter((int)Classes.parameter.HEAT_SOURCE), J, b_i;
            J = calculateLocalJ(element, m);

            b_i = Q * J / 24.0f;
            b.Add(b_i); b.Add(b_i);
            b.Add(b_i); b.Add(b_i);

            return b;
        }

        public void crearSistemasLocales(Classes.mesh m, List<Matrix> localKs, List<Vector> localbs)
        {
            for (int i = 0; i < m.getSize((int)Classes.size.ELEMENTS); i++)
            {
                localKs.Add(createLocalK(i, m));
                localbs.Add(createLocalb(i, m));
            }
        }

        public void assemblyK(Classes.element e, Matrix localK, Matrix K)
        {
            int index1 = e.node1 - 1;
            int index2 = e.node2 - 1;
            int index3 = e.node3 - 1;
            int index4 = e.node4 - 1;

            K[index1][index1] += localK[0][0];
            K[index1][index2] += localK[0][1];
            K[index1][index3] += localK[0][2];
            K[index1][index4] += localK[0][3];
            K[index2][index1] += localK[1][0];
            K[index2][index2] += localK[1][1];
            K[index2][index3] += localK[1][2];
            K[index2][index4] += localK[1][3];
            K[index3][index1] += localK[2][0];
            K[index3][index2] += localK[2][1];
            K[index3][index3] += localK[2][2];
            K[index3][index4] += localK[2][3];
            K[index4][index1] += localK[3][0];
            K[index4][index2] += localK[3][1];
            K[index4][index3] += localK[3][2];
            K[index4][index4] += localK[3][3];
        }

        public void assemblyb(Classes.element e, Vector localb, Vector b)
        {
            int index1 = e.node1 - 1;
            int index2 = e.node2 - 1;
            int index3 = e.node3 - 1;
            int index4 = e.node4 - 1;

            b[index1] += localb[0];
            b[index2] += localb[1];
            b[index3] += localb[2];
            b[index4] += localb[3];
        }

        public void ensamblaje(Classes.mesh m, List<Matrix> localKs, List<Vector> localbs, Matrix K, Vector b)
        {
            for (int i = 0; i < m.getSize((int)Classes.size.ELEMENTS); i++)
            {
                Classes.element e = m.getElement(i);
                assemblyK(e, localKs.ElementAt(i), K);//Si tuesta, poner localKs[i]
                assemblyb(e, localbs.ElementAt(i), b);
            }
        }

        public void applyNeumann(Classes.mesh m, Vector b)
        {
            for (int i = 0; i < m.getSize((int)Classes.size.NEUMANN); i++)
            {
                Classes.condition c = m.getCondition(i, (int)Classes.size.NEUMANN);
                b[(c.node1 - 1)] += c.value;
            }
        }

        public void applyDirichlet(Classes.mesh m, Matrix K, Vector b)
        {
            for (int i = 0; i < m.getSize((int)Classes.size.DIRICHLET); i++)
            {
                Classes.condition c = m.getCondition(i, (int)Classes.size.DIRICHLET);
                int index = c.node1 - 1;

                K.RemoveAt(index);
                b.RemoveAt(index);

                for (int row = 0; row < K.Count; row++)
                {
                    float cell = K.ElementAt(row).ElementAt(index);
                    K.ElementAt(row).RemoveAt(index);
                    b[row] += -1 * c.value * cell;
                }
            }
        }

        public void calculate(Matrix K, Vector b, Vector T)
        {
            Console.WriteLine("Iniciando calculo de respuesta...");
            Matrix Kinv = new Matrix();

            Console.WriteLine("Calculo de inversa...");
            Math_tools.inverseMatrix(K, Kinv);//TODO 

            Console.WriteLine("Calculo de respuesta...");
            Math_tools.productMatrixVector(Kinv, b, T);//TODO
        }
    }
}
