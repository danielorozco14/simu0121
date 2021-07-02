using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace MEF3D
{
    class Tools
    {
        public static void obtenerDatos(string filename, int nlines, int n, int mode, List<Classes.item> items_list)
        {
            try
            {
                using (StreamReader sr = new StreamReader(filename))
                {
                    string[] lines = File.ReadAllLines(filename);
                    //System.Console.WriteLine(lines[0]);
                    foreach (string item in lines)
                    {
                        string[] splitted = item.Split(new Char[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string spl in splitted)
                        {
                            if (spl != " ")
                                System.Console.WriteLine(spl);

                        }

                    }
                    /*
                    while((line = sr.ReadLine()) != null){
                        System.Console.WriteLine(line);
                    }
                    */
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
