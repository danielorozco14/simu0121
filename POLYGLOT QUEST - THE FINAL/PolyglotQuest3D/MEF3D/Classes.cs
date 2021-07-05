using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF3D
{
    

    public class Classes
    {
        public enum indicator { NOTHING };
        public enum line { NOLINE, SINGLELINE, DOUBLELINE };

        //TODO: MODIFIED MODE ENUM TO SAVE MORE MODES
        public enum mode { NOMODE, INT_FLOAT, INT_FLOAT_FLOAT_FLOAT, INT_INT_INT_INT_INT };
        public enum parameter { THERMAL_CONDUCTIVITY, HEAT_SOURCE };
        public enum size { NODES, ELEMENTS, DIRICHLET, NEUMANN };

        /*
        public class item
        {
            public int id { get; set; }
            public float x { get; set; }
            public float y { get; set; }
            public float z { get; set; }
            public int node1 { get; set; }
            public int node2 { get; set; }
            public int node3 { get; set; }
            public int node4 { get; set; }
            public float value { get; set; }

            public virtual void setValues(int a, float b, float c, float d, int e, int f, int g, int h, float i) { }
        }

        public class node : item
        {
            public override void setValues(int a, float b, float c, float d, int e, int f, int g, int h, float i)
            {
                id = a;
                x = b;
                y = c;
                z = d;
            }
        }

        public class element : item
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

        public class condition : item{

            public override void setValues(int a, float b, float c, float d, int e, int f, int g, int h, float i)
            {
                node1 = e;
                value = i;
            }

        }

        public class mesh
        {
            public float[]  parameters = new float [2];
            public int[] sizes = new int[4];
            public int[] indices_dirich;

            public node[] node_list;
            public element[] element_list;
            public condition[] dirichlet_list;
            public condition[] neumann_list;

            public void setParameters(float k, float Q)
            {
                parameters[(int)parameter.THERMAL_CONDUCTIVITY] = k;
                parameters[(int)parameter.HEAT_SOURCE] = Q;
            }
            public void setSizes(int nnodes, int neltos, int ndirich, int nneu)
            {
                sizes[(int)size.NODES] = nnodes;
                sizes[(int)size.ELEMENTS] = neltos;
                sizes[(int)size.DIRICHLET] = ndirich;
                sizes[(int)size.NEUMANN] = nneu;
            }
            public int getSize(int s)
            {
                return sizes[s];
            }
            public float getParameter(int p)
            {
                return parameters[p];
            }
            public void createData()
            {
                node_list = new node[(int)size.NODES];
                element_list = new element[(int)size.ELEMENTS];
                indices_dirich = new int[(int)size.DIRICHLET];
                dirichlet_list = new condition[(int)size.DIRICHLET];
                neumann_list = new condition[(int)size.NEUMANN];
            }
            public node[] getNodes()
            {
                return node_list;
            }
            public element[] getElements()
            {
                return element_list;
            }
            public int[] getDirichletIndices()
            {
                return indices_dirich;
            }
            public condition[] getDirichlet()
            {
                return dirichlet_list;
            }
            public condition[] getNeumann()
            {
                return neumann_list;
            }
            public node getNode(int i)
            {
                return node_list[i];
            }
            public element getElement(int i)
            {
                return element_list[i];
            }
            public condition getCondition(int i, int type)
            {
                if (type == (int)size.DIRICHLET) return dirichlet_list[i];
                else return neumann_list[i];
            }


        }

        */
    }
}
