using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF3D.Helpers
{
   public class Element:Item
    {
        public override void setValues(int a, float b, float c, float d, int e, int f, int g, int h,
                int j, int k, int l, int m, int n, int o, float i)
        {
            id = a;
            node1 = e;
            node2 = f;
            node3 = g;
            node4 = h;
            node5 = j;
            node6 = k;
            node7 = l;
            node8 = m;
            node9 = n;
            node10 = o;
            



        }
    }
}
