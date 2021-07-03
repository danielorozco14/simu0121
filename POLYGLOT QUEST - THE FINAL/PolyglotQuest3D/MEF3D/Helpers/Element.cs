using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF3D.Helpers
{
   public class Element:Item
    {
        public override void setValues(int a, float b, float c, float d, int e, int f, int g, int h, float i)
        {
            id = a;
            node1 = e;
            node2 = f;
            node3 = g;
            node4 = h;

        }
    }
}
