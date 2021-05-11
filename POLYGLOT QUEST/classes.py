from enum import Enum

## enums

Lines = Enum('Lines', 'NOLINE SINGLELINE DOUBLELINE')
Modes = Enum ('Modes', 'NOMODE INT_FLOAT INT_INT_INT')
Parameters = Enum('Parameters', 'ELEMENT_LENGTH THERMAL_CONDUCTIVITY HEAT_SOURCE')
Sizes = Enum('Sizes', 'NODES ELEMENTS DIRICHLET NEUMANN')

class item:
    _id = 0
    _x = 0.0
    _node1 = 0
    _node2 = 0
    _value = 0.0

    def getId(self):
        return self._id
    
    def getX(self):
        return self._x
    
    def getNode1(self):
        return self._node1
    
    def getNode2(self):
        return self._node2
    
    def getValue(self):
        return self._value

    def sentIntFloat(self, n, r):
        return 0
   
    def sentIntIntInt(self, n1, n2, n3):
        return 0


class Node (item):

    def sentIntFloat(self, identifier, x_coordinate):
        self._id = identifier
        self._x = x_coordinate

    def sentIntIntInt(self, n1, n2, n3):
        pass

class Element(item):
    
    def sentIntFloat(self, n1, r):
        pass
    
    def sentIntIntInt(self, identifier, firstNode, secondNode):
        self._id = identifier
        self._node1 = firstNode
        self._node2 = secondNode

class Condition(item):
    
    def sentIntFloat(self, node_to_apply, prescribed_value):
        self._node1 = node_to_apply
        self._value = prescribed_value
    
    def sentIntIntInt(self, identifier, firstNode, secondNode):
        pass

class Mesh:
    parameters = []
    node_list = Node()
    element_list = Element()
    dirichlet_list = Condition()
    neumann_list = Condition()

    def setParameters(self, l, k, Q):
        
        self.parameters.insert(Parameters.ELEMENT_LENGTH.value -1,l)
        self.parameters.insert(Parameters.THERMAL_CONDUCTIVITY.value - 1, k)
        self.parameters.insert(Parameters.HEAT_SOURCE.value - 1, Q)
    
    def setSizes(self, nNodes, nElemts, nDirich, nNeumn):
        ## AQUI ESTA EL ERROR,
        # HAY QUE BUSCAR COMO CAMBIAR EL VALOR DE UN ENUM LUEGO DE SER DECLARADO
        Sizes.NODES.value = nNodes
        Sizes.ELEMENTS.value = nElemts
        Sizes.DIRICHLET.value = nDirich
        Sizes.NEUMANN.value = nNeumn

    def getSize(self, s):
        return self.sizes[s]

    def getParameter(self, p):
        return self.sizes[p]

    ##SI REVIENTA ES POR ESTE METODO
    def createData(self):
        print(Sizes.NODES.value)
        self.node_list =  [Node()] * Sizes.NODES.value
        self.element_list = [Element()] * Sizes.ELEMENTS.value
        self.dirichlet_list = [Condition()] * Sizes.DIRICHLET.value
        self.neumann_list = [Condition()] * Sizes.NEUMANN.value
        

    def getNodes(self):
        return self.node_list
    
    def getElements(self):
        return self.element_list

    def getDirichlet(self):
        return self.dirichlet_list

    def getNeumann(self):
        return self.neumann_list

    def getNode(self, i):
        return self.node_list[i]
    
    def getElement(self, i):
        return self.element_list[i]
    
    def getCondition(self, i, type):
        #Talvez pete aqui, agregar el .value al Sizes.DIRICHLET
        if(type == Sizes.DIRICHLET):
            return self.dirichlet_list[i]
        else:
            return self.neumann_list[i]