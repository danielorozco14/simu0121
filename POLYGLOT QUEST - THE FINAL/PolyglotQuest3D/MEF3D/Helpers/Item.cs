using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF3D.Helpers
{
    public class Item
    {
       
            public int id { get; set; }
            public float x { get; set; }
            public float y { get; set; }
            public float z { get; set; }
            public int node1 { get; set; }
            public int node2 { get; set; }
            public int node3 { get; set; }
            public int node4 { get; set; }
            public int node5 { get; set; }
            public int node6 { get; set; }
            public int node7 { get; set; }
            public int node8 { get; set; }
            public int node9 { get; set; }
            public int node10 { get; set; }
            public float value { get; set; }

            public virtual void setValues(int a, float b, float c, float d, int e, int f, int g, int h,
                int j, int k, int l, int m, int n, int o, float i) { }
        
    }
}
