using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEF3D.Helpers
{
    public class Mesh 
    {
        public float[] parameters = new float[2];
        public int[] sizes = new int[4];//4
        public int[] indices_dirich;

        public Node[] node_list;
        public Element[] element_list;
        public Condition[] dirichlet_list;
        public Condition[] neumann_list;

        public void setParameters(float k, float Q)
        {
            parameters[(int)Classes.parameter.THERMAL_CONDUCTIVITY] = k;
            parameters[(int)Classes.parameter.HEAT_SOURCE] = Q;
        }
        public void setSizes(int nnodes, int neltos, int ndirich, int nneu)
        {
            sizes[(int)Classes.size.NODES] = nnodes;
            sizes[(int)Classes.size.ELEMENTS] = neltos;
            sizes[(int)Classes.size.DIRICHLET] = ndirich;
            sizes[(int)Classes.size.NEUMANN] = nneu;
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
            node_list = new Node[sizes[(int)Classes.size.NODES]];
            element_list = new Element[sizes[(int)Classes.size.ELEMENTS]];
            indices_dirich = new int[sizes[(int)Classes.size.DIRICHLET]];
            dirichlet_list = new Condition[sizes[(int)Classes.size.DIRICHLET]];
            neumann_list = new Condition[sizes[(int)Classes.size.NEUMANN]];

            for (int i =0; i < sizes[0]; i++)
            {
                Node node = new Node();
                node_list[i] = node;
            }

            for (int i = 0; i < sizes[1]; i++)
            {
                Element element = new Element();
                element_list[i] = element;
            }

            for (int i = 0; i < sizes[2]; i++)
            {
                indices_dirich[i] = 0;
            }

            for (int i = 0; i < sizes[2]; i++)
            {
                Condition dirichlet = new Condition();
                dirichlet_list[i] = dirichlet;
            }

            for (int i = 0; i < sizes[3]; i++)
            {
                Condition neumann = new Condition();
                neumann_list[i] = neumann;
            }

        }
        public Node[] getNodes()
        {
            return node_list;
        }
        public Element[] getElements()
        {
            return element_list;
        }
        public int[] getDirichletIndices()
        {
            return indices_dirich;
        }
        public Condition[] getDirichlet()
        {
            return dirichlet_list;
        }
        public Condition[] getNeumann()
        {
            return neumann_list;
        }
        public Node getNode(int i)
        {
            return node_list[i];
        }
        public Element getElement(int i)
        {
            return element_list[i];
        }
        public Condition getCondition(int i, int type)
        {
            if (type == (int)Classes.size.DIRICHLET) return dirichlet_list[i];
            else return neumann_list[i];
        }

    }
}
