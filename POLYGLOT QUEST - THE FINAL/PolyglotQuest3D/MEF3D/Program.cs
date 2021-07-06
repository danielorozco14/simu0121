using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEF3D.Helpers;

namespace MEF3D
{
    using Vector = List<float>;
    using Matrix = List<List<float>>;
    class Program
    {
        static void Main(string[] args)
        {
            string filename = args[0];

            List<Matrix> localKs = new List<Matrix>();
            List<Vector> localbs = new List<Vector>();


            Matrix K = new Matrix();
            Vector b = new Vector();
            Vector T = new Vector();
            Sel sel = new Sel();

            Console.Write("IMPLEMENTACION DEL METODO DE LOS ELEMENTOS FINITOS\n"
                + "\t- TRANSFERENCIA DE CALOR\n" + "\t- 3 DIMENSIONES\n" +
                "\t- FUNCIONES DE FORMA LINEALES\n" + "\t- PESOS DE GALERKIN\n"
                + "\t- ELEMENTOS TETRAHEDROS\n"+
                "*********************************************************************************\n\n");

            Mesh  m = new Mesh();

            Tools.leerMallayCondiciones(m, filename + ".dat");
            Console.WriteLine("Datos obtenidos correctamente");

            sel.crearSistemasLocales(m, localKs, localbs);

            Math_tools.zeroes(K, m.getSize((int)Classes.size.NODES));
            Math_tools.zeroes(b, m.getSize((int)Classes.size.NODES));
            
            sel.ensamblaje(m, localKs, localbs, K, b);

            sel.applyNeumann(m, b);
            sel.applyDirichlet(m, K, b);

            Math_tools.zeroes(T, b.Count);
            sel.calculate(K, b, T);

            Tools.writeResults(m, T, filename);

        }
    }
}
