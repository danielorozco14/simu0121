using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF3D.Helpers
{
    public class Node:Item
    {
       
            public override void setValues(int a, float b, float c, float d, int e, int f, int g, int h, float i)
            {
                id = a;
                x = b;
                y = c;
                z = d;
            }
        
    }
}
