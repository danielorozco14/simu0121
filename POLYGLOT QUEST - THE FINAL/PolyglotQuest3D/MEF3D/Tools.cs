using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using MEF3D.Helpers;

namespace MEF3D
{
    using Vector = List<float>;
    class Tools
    {
        //HAY QUE ARREGLAR EL SALTO DE LINEA Y EL GETNODE(I) DE LOCALSD

        Classes classes = new Classes();
        public static void obtenerDatos(StreamReader sr, int nlines, int n, int mode, Item[] items_list)
        {
            try
            {
                string line;
                string[] resultLine;

                sr.ReadLine();
                sr.ReadLine();

                if (nlines == (int)Classes.line.DOUBLELINE)
                    sr.ReadLine();
                //Item item = new Item();
                for (int i = 0; i < n; i++)
                {
                    switch (mode)
                    {
                        case ((int)Classes.mode.INT_FLOAT):
                            int e0; float r0;
                            line = sr.ReadLine();
                            resultLine = line.Split(new Char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);

                            e0 = Convert.ToInt32(resultLine[0]);
                            r0 = float.Parse(resultLine[1]);
                            
                            //item = items_list[i];
                            //item.setValues(0, 0, 0, 0, e0, 0, 0, 0, r0);
                            //CHANGE POSITION OF ARGUMENTS
                            items_list[i].setValues(0, 0, 0, 0, e0, 0, 0, 0, 0, 0, 0, 0, 0, 0, r0);
                           // System.Console.WriteLine("e0 :" + e0 + " r0 : " + r0);

                            break;

                        case ((int)Classes.mode.INT_FLOAT_FLOAT_FLOAT)://THIS ONE READS COORDINATES
                            int e; float r, rr, rrr;
                            line = sr.ReadLine();
                            resultLine = line.Split(new Char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                            
                            e = Convert.ToInt32(resultLine[0]);
                            r = float.Parse(resultLine[1]);
                            rr = float.Parse(resultLine[2]);
                            rrr = float.Parse(resultLine[3]);
                         
                            items_list[i].setValues(e, r, rr, rrr, 0, 0, 0, 0,0,0,0,0,0,0, 0);

                            break;
                           
                        case ((int)Classes.mode.INT_INT_INT_INT_INT)://THIS ONE READS ELEMENTS
                            int e1, e2, e3, e4, e5, e6, e7, e8, e9 , e10, e11;
                            line = sr.ReadLine();
                            resultLine = line.Split(new Char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);

                            e1 = Convert.ToInt32(resultLine[0]);//ID
                            e2 = Convert.ToInt32(resultLine[1]);
                            e3 = Convert.ToInt32(resultLine[2]);
                            e4 = Convert.ToInt32(resultLine[3]);
                            e5 = Convert.ToInt32(resultLine[4]);
                            e6 = Convert.ToInt32(resultLine[5]);
                            e7 = Convert.ToInt32(resultLine[6]);
                            e8 = Convert.ToInt32(resultLine[7]);
                            e9 = Convert.ToInt32(resultLine[8]);
                            e10 = Convert.ToInt32(resultLine[9]);
                            e11 = Convert.ToInt32(resultLine[10]);
                            
                            items_list[i].setValues(e1, 0, 0, 0, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, 0);

                            break;
                    }
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message + e.StackTrace);
            }
        }


        public static void correctConditions(int n, Condition[] list, int[] indices)
        {
            for(int i =0; i < n; i++)
            {
                indices[i] = list[i].node1;
            }

            for(int i =0; i< n -1; i++)
            {
                int pivot = list[i].node1;
                for(int j =i; j < n; j++)
                {
                    //Si la condición actual corresponde a un nodo posterior al nodo eliminado por
                    //aplicar la condición anterior, se debe actualizar su posición.
                    if (list[j].node1 > pivot)
                        list[j].node1 = list[j].node1 - 1;
                }
            }
        }

        public static void leerMallayCondiciones(Mesh m, string filename)
        {
            try
            {
                StreamReader sr = new StreamReader(filename);
                float k, Q;
                int nnodes, neltos, ndirich, nneu;
                string[] result;
                //addExtension(inputFileName, filename, ".dat");

                result= sr.ReadLine().Split(new Char[] { ' '}, StringSplitOptions.RemoveEmptyEntries);
                
                k = float.Parse(result[0]);
                Q = float.Parse(result[1]);

                result = sr.ReadLine().Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                nnodes = Convert.ToInt32(result[0]);
                neltos = Convert.ToInt32(result[1]);
                ndirich = Convert.ToInt32(result[2]);
                nneu = Convert.ToInt32(result[3]);
                
                m.setParameters(k, Q);
                m.setSizes(nnodes * 3, neltos, ndirich, nneu);
                m.createData();

                obtenerDatos(sr, (int)Classes.line.SINGLELINE, nnodes, (int)Classes.mode.INT_FLOAT_FLOAT_FLOAT, m.getNodes());
                obtenerDatos(sr, (int)Classes.line.DOUBLELINE, neltos, (int)Classes.mode.INT_INT_INT_INT_INT, m.getElements());

                obtenerDatos(sr, (int)Classes.line.DOUBLELINE, ndirich, (int)Classes.mode.INT_FLOAT, m.getDirichlet());
                obtenerDatos(sr, (int)Classes.line.DOUBLELINE, nneu, (int)Classes.mode.INT_FLOAT, m.getNeumann());
               
                sr.Close();

                correctConditions(ndirich, m.getDirichlet(), m.getDirichletIndices());


            }catch(Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace);
            }
        }


        public static bool findIndex(int v, int s, int[] arr)
        {
            for (int i = 0; i < s; i++)
                if (arr[i] == v) return true;
            return false;
        }

        public static void writeResults(Mesh m, Vector T, string filename)
        {
            int[] dirich_indices = m.getDirichletIndices();
            Condition[] dirich = m.getDirichlet();

            try
            {
                StreamWriter streamWriter = new StreamWriter(filename + ".post.res");
                streamWriter.WriteLine("GiD Post Results File 1.0\n");
                streamWriter.WriteLine("Result \"Temperature\" \"Load Case 1\" 1 Scalar OnNodes\nComponentNames \"T\"\nValues");

                int Tpos = 0;
                int Dpos = 0;

                int n = m.getSize((int)Classes.size.NODES);
                int nd = m.getSize((int)Classes.size.DIRICHLET);

                for(int i =0; i < n; i++)
                {
                    if (findIndex(i + 1, nd, dirich_indices))
                    {
                        streamWriter.WriteLine(i + 1 + " " + dirich[Dpos].value);
                        
                        Dpos++;
                    }
                    else
                    {
                        streamWriter.WriteLine(i + 1 + " " + T[(Tpos)]);
                        Tpos++;
                    }
                }

                streamWriter.WriteLine("End Values");

                streamWriter.Close();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message + e.StackTrace); 
            }


        }


    }
}
