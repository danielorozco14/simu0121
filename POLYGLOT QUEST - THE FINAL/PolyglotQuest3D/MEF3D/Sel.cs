using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF3D.Helpers;

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

        public Matrix createLocalK(int id, Mesh m)//id = element
        {
            
            Matrix localK = new Matrix();
            float EI = 88f; //VALOR QUEMADO PORQUE ES EL RANDOMIZADO
            float Jacobian = calculateLocalJ(id, m);

            ////
            float A = calculateElement_A(id, m);
            float B = calculateElement_B(id, m);
            float C = calculateElement_C(id, m);
            float D = calculateElement_D(id, m);
            float E = calculateElement_E(id, m);
            float F = calculateElement_F(id, m);
            float G = calculateElement_G(id, m);
            float H = calculateElement_H(id, m);
            float I = calculateElement_I(id, m);
            float J = calculateElement_J(id, m);
            float K = calculateElement_K(id, m);

            Math_tools.zeroes(localK, 30);


            //SI TRUENA, ES PORQUE SE DURMIERON Y ME DICTARON MAL MATRIZ MIU
            //COPY 1
            localK[0][0] = (EI * Jacobian) * A;
            localK[0][1] = (EI * Jacobian) * E;
            localK[0][4] = (EI * Jacobian) * -E;
            localK[0][6] = (EI * Jacobian) * -F;
            localK[0][7] = (EI * Jacobian) * G;
            localK[0][8] = (EI * Jacobian) * F;
            localK[0][9] = (EI * Jacobian) * F;

            localK[1][0] = (EI * Jacobian) * E;
            localK[1][1] = (EI * Jacobian) * B;
            localK[1][4] = (EI * Jacobian) * -H;
            localK[1][6] = (EI * Jacobian) * -H;
            localK[1][7] = (EI * Jacobian) * I;
            localK[1][8] = (EI * Jacobian) * H;
            localK[1][9] = (EI * Jacobian) * H;

            localK[4][0] = (EI * Jacobian) * -F;
            localK[4][1] = (EI * Jacobian) * -H;
            localK[4][4] = (EI * Jacobian) * C;
            localK[4][6] = (EI * Jacobian) * J;
            localK[4][7] = (EI * Jacobian) * -K;
            localK[4][8] = (EI * Jacobian) * -C;
            localK[4][9] = (EI * Jacobian) * -J;

            localK[6][0] = (EI * Jacobian) * -F;
            localK[6][1] = (EI * Jacobian) * -H;
            localK[6][4] = (EI * Jacobian) * J;
            localK[6][6] = (EI * Jacobian) * C;
            localK[6][7] = (EI * Jacobian) * -K;
            localK[6][8] = (EI * Jacobian) * -J;
            localK[6][9] = (EI * Jacobian) * -C;

            localK[7][0] = (EI * Jacobian) * G;
            localK[7][1] = (EI * Jacobian) * I;
            localK[7][4] = (EI * Jacobian) * -K;
            localK[7][6] = (EI * Jacobian) * -K;
            localK[7][7] = (EI * Jacobian) * D;
            localK[7][8] = (EI * Jacobian) * K;
            localK[7][9] = (EI * Jacobian) * K;

            localK[8][0] = (EI * Jacobian) * F;
            localK[8][1] = (EI * Jacobian) * H;
            localK[8][4] = (EI * Jacobian) * -C;
            localK[8][6] = (EI * Jacobian) * -J;
            localK[8][7] = (EI * Jacobian) * K;
            localK[8][8] = (EI * Jacobian) * C;
            localK[8][9] = (EI * Jacobian) * J;

            localK[9][0] = (EI * Jacobian) * F;
            localK[9][1] = (EI * Jacobian) * H;
            localK[9][4] = (EI * Jacobian) * -J;
            localK[9][6] = (EI * Jacobian) * -C;
            localK[9][7] = (EI * Jacobian) * K;
            localK[9][8] = (EI * Jacobian) * J;
            localK[9][9] = (EI * Jacobian) * C;

            //COPY 2
            localK[10][10] = (EI * Jacobian) * A;
            localK[10][11] = (EI * Jacobian) * E;
            localK[10][14] = (EI * Jacobian) * -E;
            localK[10][16] = (EI * Jacobian) * -F;
            localK[10][17] = (EI * Jacobian) * G;
            localK[10][18] = (EI * Jacobian) * F;
            localK[10][19] = (EI * Jacobian) * F;

            localK[11][10] = (EI * Jacobian) * E;
            localK[11][11] = (EI * Jacobian) * B;
            localK[11][14] = (EI * Jacobian) * -H;
            localK[11][16] = (EI * Jacobian) * -H;
            localK[11][17] = (EI * Jacobian) * I;
            localK[11][18] = (EI * Jacobian) * H;
            localK[11][19] = (EI * Jacobian) * H;

            localK[14][10] = (EI * Jacobian) * -F;
            localK[14][11] = (EI * Jacobian) * -H;
            localK[14][14] = (EI * Jacobian) * C;
            localK[14][16] = (EI * Jacobian) * J;
            localK[14][17] = (EI * Jacobian) * -K;
            localK[14][18] = (EI * Jacobian) * -C;
            localK[14][19] = (EI * Jacobian) * -J;

            localK[16][10] = (EI * Jacobian) * -F;
            localK[16][11] = (EI * Jacobian) * -H;
            localK[16][14] = (EI * Jacobian) * J;
            localK[16][16] = (EI * Jacobian) * C;
            localK[16][17] = (EI * Jacobian) * -K;
            localK[16][18] = (EI * Jacobian) * -J;
            localK[16][19] = (EI * Jacobian) * -C;

            localK[17][10] = (EI * Jacobian) * G;
            localK[17][11] = (EI * Jacobian) * I;
            localK[17][14] = (EI * Jacobian) * -K;
            localK[17][16] = (EI * Jacobian) * -K;
            localK[17][17] = (EI * Jacobian) * D;
            localK[17][18] = (EI * Jacobian) * K;
            localK[17][19] = (EI * Jacobian) * K;

            localK[18][10] = (EI * Jacobian) * F;
            localK[18][11] = (EI * Jacobian) * H;
            localK[18][14] = (EI * Jacobian) * -C;
            localK[18][16] = (EI * Jacobian) * -J;
            localK[18][17] = (EI * Jacobian) * K;
            localK[18][18] = (EI * Jacobian) * C;
            localK[18][19] = (EI * Jacobian) * J;

            localK[19][10] = (EI * Jacobian) * F;
            localK[19][11] = (EI * Jacobian) * H;
            localK[19][14] = (EI * Jacobian) * -J;
            localK[19][16] = (EI * Jacobian) * -C;
            localK[19][17] = (EI * Jacobian) * K;
            localK[19][18] = (EI * Jacobian) * J;
            localK[19][19] = (EI * Jacobian) * C;

            //COPY 3

            localK[20][20] = (EI * Jacobian) * A;
            localK[20][21] = (EI * Jacobian) * E;
            localK[20][24] = (EI * Jacobian) * -E;
            localK[20][26] = (EI * Jacobian) * -F;
            localK[20][27] = (EI * Jacobian) * G;
            localK[20][28] = (EI * Jacobian) * F;
            localK[20][29] = (EI * Jacobian) * F;

            localK[21][20] = (EI * Jacobian) * E;
            localK[21][21] = (EI * Jacobian) * B;
            localK[21][24] = (EI * Jacobian) * -H;
            localK[21][26] = (EI * Jacobian) * -H;
            localK[21][27] = (EI * Jacobian) * I;
            localK[21][28] = (EI * Jacobian) * H;
            localK[21][29] = (EI * Jacobian) * H;

            localK[24][20] = (EI * Jacobian) * -F;
            localK[24][21] = (EI * Jacobian) * -H;
            localK[24][24] = (EI * Jacobian) * C;
            localK[24][26] = (EI * Jacobian) * J;
            localK[24][27] = (EI * Jacobian) * -K;
            localK[24][28] = (EI * Jacobian) * -C;
            localK[24][29] = (EI * Jacobian) * -J;

            localK[26][20] = (EI * Jacobian) * -F;
            localK[26][21] = (EI * Jacobian) * -H;
            localK[26][24] = (EI * Jacobian) * J;
            localK[26][26] = (EI * Jacobian) * C;
            localK[26][27] = (EI * Jacobian) * -K;
            localK[26][28] = (EI * Jacobian) * -J;
            localK[26][29] = (EI * Jacobian) * -C;

            localK[27][20] = (EI * Jacobian) * G;
            localK[27][21] = (EI * Jacobian) * I;
            localK[27][24] = (EI * Jacobian) * -K;
            localK[27][26] = (EI * Jacobian) * -K;
            localK[27][27] = (EI * Jacobian) * D;
            localK[27][28] = (EI * Jacobian) * K;
            localK[27][29] = (EI * Jacobian) * K;

            localK[28][20] = (EI * Jacobian) * F;
            localK[28][21] = (EI * Jacobian) * H;
            localK[28][24] = (EI * Jacobian) * -C;
            localK[28][26] = (EI * Jacobian) * -J;
            localK[28][27] = (EI * Jacobian) * K;
            localK[28][28] = (EI * Jacobian) * C;
            localK[28][29] = (EI * Jacobian) * J;

            localK[29][20] = (EI * Jacobian) * F;
            localK[29][21] = (EI * Jacobian) * H;
            localK[29][24] = (EI * Jacobian) * -J;
            localK[29][26] = (EI * Jacobian) * -C;
            localK[29][27] = (EI * Jacobian) * K;
            localK[29][28] = (EI * Jacobian) * J;
            localK[29][29] = (EI * Jacobian) * C;

            return localK;
        }

        public float calculateLocalC1(int id, Mesh m)
        {
            Element el = m.getElement(id);
            float result = 0.0f;

            Node n1 = m.getNode(el.node1 - 1);
           
            Node n2 = m.getNode(el.node2 - 1);

            if (n1.x == n2.x)
                return (float)Math.Pow(10, -6);
            

            result = (float)(1 / Math.Pow((n2.x - n1.x), 2));


            return result;
        }

        

        public float calculateLocalC2(int id, Mesh m)
        {
            Element el = m.getElement(id);
            float result = 0.0f;

            Node n1 = m.getNode(el.node1 - 1);
            Node n2 = m.getNode(el.node2 - 1);
            Node n8 = m.getNode(el.node8 - 1);

            if (n1.x == n2.x || (4 * n1.x + 4 * n2.x - (8 * n8.x)) == 0)
                return (float)Math.Pow(10, -6);

            result = (float)((1 / ( (n2.x - n1.x)))) * (4 * n1.x + 4 * n2.x - ( 8 * n8.x));

            return result;

        }


        public float calculateElement_K(int id, Mesh m)
        {
            return (-4 / 3) * calculateLocalC1(id, m) * calculateLocalC2(id, m);
        }


        public float calculateElement_J(int id, Mesh m)
        {
            return (2 / 15) * (float)Math.Pow( calculateLocalC2(id, m), 2);
        }


        public float calculateElement_I(int id, Mesh m)
        {
            return ((-16 / 3) * (float)Math.Pow(calculateLocalC1(id, m), 2)) - ((2 / 3) * (float)Math.Pow(calculateLocalC2(id, m), 2));
        }

        public float calculateElement_H(int id, Mesh m)
        {
            float c1 = calculateLocalC1(id, m);
            float c2 = calculateLocalC2(id, m);

            return (2 / 3 * c1 * c2) + (1 / 30 * (float)Math.Pow(c2, 2));
        }

        public float calculateElement_G(int id, Mesh m)
        {
            float c1 = calculateLocalC1(id, m);
            float c2 = calculateLocalC2(id, m);

            return (-16 / 3 * (float)Math.Pow(c1, 2)) - (4 / 3 * c1 * c2) - (2/15 * (float)Math.Pow(c2,2));
        }

        public float calculateElement_F(int id, Mesh m)
        {
            float c1 = calculateLocalC1(id, m);
            float c2 = calculateLocalC2(id, m);

            return (2 / 3 * c1 * c2) - (1/30 * (float)Math.Pow(c2, 2));
        }

        public float calculateElement_E(int id, Mesh m)
        {
            float c1 = calculateLocalC1(id, m);
            float c2 = calculateLocalC2(id, m);

            return (8 / 3 *(float)Math.Pow(c1, 2)) + (1 / 30 * (float)Math.Pow(c2, 2));
        }

        public float calculateElement_D(int id, Mesh m)
        {
            float c1 = calculateLocalC1(id, m);
            float c2 = calculateLocalC2(id, m);

            return (1 / (192 * (float)Math.Pow(4 * c2, 2))) * (float)Math.Pow((4 * c2 - c1), 4)
                - (1 / (3840 * (float)Math.Pow(c2, 3))) * (float)Math.Pow((4 * c2 - c1), 5)
                + (1 / (7680 * (float)Math.Pow(c2, 3))) * (float)Math.Pow((4 * c2 + 8 * c1), 5)
                - (7 / (7680 * (float)Math.Pow(c2, 3))) * (float)Math.Pow((4 * c2 + 8 * c1), 5)
                + (1 / ( 768 * (float)Math.Pow(c2, 3))) * (float)Math.Pow((-8 * c1), 5)
                - (c1 / ( 96  * (float)Math.Pow(c2, 3))) * (float)Math.Pow((4 * c2 - 8 * c1), 4)
                + (2 * c1 - 1 / (192 * (float)Math.Pow(c2, 3))) * (float)Math.Pow((-8 * c1), 4)
                ;
        }


        public float calculateElement_C(int id, Mesh m)
        {
            float c2 = calculateLocalC2(id, m);
            return (4 / 15 * (float) Math.Pow(c2, 2));
        }

        public float calculateElement_B(int id, Mesh m)
        {
            float c1 = calculateLocalC1(id, m);
            float c2 = calculateLocalC2(id, m);


            return (-1 / (192 * (float)Math.Pow(c2, 2)) * (float)Math.Pow((4 * c1 + c2),4) 
                + (1 / (24 * c2)    )                   * (float)Math.Pow((4 * c1 + c2), 3))
                + (1 / (3840 * (float)Math.Pow(c2, 3))  * (float)Math.Pow((4 * c1 + c2), 5)
                - (1 / (3840 * (float)Math.Pow(c2, 3))  * (float)Math.Pow((4 * c1 - 3 * c2), 5)
                ));
        }

        public float calculateElement_A(int id, Mesh m)
        {
            float c1 = calculateLocalC1(id, m);
            float c2 = calculateLocalC2(id, m);

            return (-1 / (192 * (float)Math.Pow(c2, 2)) * (float)Math.Pow((4 * c1 - c2), 4) 
                 - (1 / (24 * c2))                      * (float)Math.Pow((4 * c1 - c2), 3))
                 - (1 / (3840 * (float)Math.Pow(c2, 3)) * (float)Math.Pow((4 * c1 - c2), 5))
                 + (1 / (3840 * (float)Math.Pow(c2, 3)) * (float)Math.Pow((4 * c1 + 3*c2), 5))
                ;
        }

        public float calculateLocalJ(int ind, Mesh m)
        {
            float J, a, b, c, d, e, f, g, h, i;

            Element el = m.getElement(ind);

            Node n1 = m.getNode(el.node1 - 1);
            Node n2 = m.getNode(el.node2 - 1);
            Node n3 = m.getNode(el.node3 - 1);
            Node n4 = m.getNode(el.node4 - 1);

            a = n2.x - n1.x; b = n3.x - n1.x; c = n4.x - n1.x;
            d = n2.y - n1.y; e = n3.y - n1.y; f = n4.y - n1.y;
            g = n2.z - n1.z; h = n3.z - n1.z; i = n4.z - n1.z;

            //Se calcula el determinante de esta matriz utilizando
            //la Regla de Sarrus.
            J = a * e * i + d * h * c + g * b * f - g * e * c - a * h * f - d * b * i;

            return J;
        }

        public Vector createLocalb(int element, Mesh m)
        {
            Vector b = new Vector();

            float J = calculateLocalJ(element, m);
            
            Math_tools.zeroes(b, 30);
            
            //FALTA MODIFICAR, HAY QUE OBTENER LOS VALORES DESDE EL ARCHIVO PARA HACER ESTO

            b[0] = (J / 120) * 3540;
            b[1] = (J / 120) * -60;
            b[2] = (J / 120) * -60;
            b[3] = (J / 120) * -60;
            b[4] = (J / 120) * 240;
            b[5] = (J / 120) * 240;
            b[6] = (J / 120) * 240;
            b[7] = (J / 120) * 240;
            b[8] = (J / 120) * 240;
            b[9] = (J / 120) * 240;
            b[10] = (J / 120) * 177;
            b[11] = (J / 120) * -3;
            b[12] = (J / 120) * -3;
            b[13] = (J / 120) * -3;
            b[14] = (J / 120) * 12;
            b[15] = (J / 120) * 12;
            b[16] = (J / 120) * 12;
            b[17] = (J / 120) * 12;
            b[18] = (J / 120) * 12;
            b[19] = (J / 120) * 12;
            b[20] = (J / 120) * -1711;
            b[21] = (J / 120) * 29;
            b[22] = (J / 120) * 29;
            b[23] = (J / 120) * 29;
            b[24] = (J / 120) * -116;
            b[25] = (J / 120) * -116;
            b[26] = (J / 120) * -116;
            b[27] = (J / 120) * -116;
            b[28] = (J / 120) * -116;
            b[29] = (J / 120) * -116;

            return b;
        }

        public void crearSistemasLocales(Mesh m, List<Matrix> localKs, List<Vector> localbs)
        {
            for (int i = 0; i < m.getSize((int)Classes.size.ELEMENTS); i++)
            {
                localKs.Add(createLocalK(i, m));
                localbs.Add(createLocalb(i, m));
            }
        }

        public void assemblyK(Element e, Matrix localK, Matrix K)
        {
            int nnodes = K.Count / 3;
            
            int index1 = e.node1 - 1; //21
            int index2 = e.node2 - 1;
            int index3 = e.node3 - 1;
            int index4 = e.node4 - 1;
            int index5 = e.node5 - 1;
            int index6 = e.node6 - 1;
            int index7 = e.node7 - 1;
            int index8 = e.node8 - 1;
            int index9 = e.node9 - 1;
            int index10 = e.node10 - 1;

            int[] indexs = new int[30];
            indexs[0] = index1;
            indexs[1] = index2;
            indexs[2] = index3;
            indexs[3] = index4;
            indexs[4] = index5;
            indexs[5] = index6;
            indexs[6] = index7;
            indexs[7] = index8;
            indexs[8] = index9;
            indexs[9] = index10;
            indexs[10] = index1 + 10;
            indexs[11] = index2 + 10;
            indexs[12] = index3+10;
            indexs[13] = index4+10;
            indexs[14] = index5+10;
            indexs[15] = index6+10;
            indexs[16] = index7+10;
            indexs[17] = index8+10;
            indexs[18] = index9+10;
            indexs[19] = index10+10;
            indexs[20] = index1+20;
            indexs[21] = index2+20;
            indexs[22] = index3+20;
            indexs[23] = index4+20;
            indexs[24] = index5+20;
            indexs[25] = index6+20;
            indexs[26] = index7+20;
            indexs[27] = index8+20;
            indexs[28] = index9+20;
            indexs[29] = index10+20;


            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 30; j++)
                {
                    int krow = indexs[i];
                    int kcol = indexs[j];
                    K[krow][kcol] += localK[i][j];

                }

            }



        }

        public void assemblyb(Element e, Vector localb, Vector b)
        {

            int nodos = 10;

            int index1 = e.node1 - 1;
            int index2 = e.node2 - 1;
            int index3 = e.node3 - 1;
            int index4 = e.node4 - 1;
            int index5 = e.node5 - 1;
            int index6 = e.node6 - 1;
            int index7 = e.node7 - 1;
            int index8 = e.node8 - 1;
            int index9 = e.node9 - 1;
            int index10 = e.node10 - 1;

            b[index1] += localb[0];
            b[index2] += localb[1];
            b[index3] += localb[2];
            b[index4] += localb[3];
            b[index5] += localb[4];
            b[index6] += localb[5];
            b[index7] += localb[6];
            b[index8] += localb[7];
            b[index9] += localb[8];
            b[index10] += localb[9];

            b[index1 + nodos] += localb[10];
            b[index2 + nodos] += localb[11];
            b[index3 + nodos] += localb[12];
            b[index4 + nodos] += localb[13];
            b[index5 + nodos] += localb[14];
            b[index6 + nodos] += localb[15];
            b[index7 + nodos] += localb[16];
            b[index8 + nodos] += localb[17];
            b[index9 + nodos] += localb[18];
            b[index10 + nodos] += localb[19];

            b[index1 +   (nodos*2)] += localb[20];
            b[index2 +   (nodos*2)] += localb[21];
            b[index3 +   (nodos*2)] += localb[22];
            b[index4 +   (nodos*2)] += localb[23];
            b[index5 +   (nodos*2)] += localb[24];
            b[index6 +   (nodos*2)] += localb[25];
            b[index7 +   (nodos*2)] += localb[26];
            b[index8 +   (nodos*2)] += localb[27];
            b[index9 +   (nodos*2)] += localb[28];
            b[index10 +  (nodos*2)] += localb[29];


        }

        //ENSAMBLAJE DE K * T = b
        public void ensamblaje(Mesh m, List<Matrix> localKs, List<Vector> localbs, Matrix K, Vector b)
        {
            for (int i = 0; i < m.getSize((int)Classes.size.ELEMENTS); i++)
            {
                Element e = m.getElement(i);
                assemblyK(e, localKs.ElementAt(i), K);//Si tuesta, poner localKs[i]
                assemblyb(e, localbs.ElementAt(i), b);
            }
        }

        public void applyNeumann(Mesh m, Vector b)
        {
            for (int i = 0; i < m.getSize((int)Classes.size.NEUMANN); i++)
            {
                Condition c = m.getCondition(i, (int)Classes.size.NEUMANN);
                b[(c.node1 - 1)] += c.value;
            }
        }

        public void applyDirichlet(Mesh m, Matrix K, Vector b)
        {
            for (int i = 0; i < m.getSize((int)Classes.size.DIRICHLET); i++)
            {
                Condition c = m.getCondition(i, (int)Classes.size.DIRICHLET);
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
            Math_tools.inverseMatrix(K, Kinv); 

            Console.WriteLine("Calculo de respuesta...");
            Math_tools.productMatrixVector(Kinv, b, T);
        }
    }
}
