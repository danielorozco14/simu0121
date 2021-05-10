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
    sizes = []
    node_list = Node()
    element_list = Element()
    dirichlet_list = Condition()
    neumann_list = Condition()

    def setParameters(self, l, k, Q):
        self.parameters[Parameters.ELEMENT_LENGTH] = l
        self.parameters[Parameters.THERMAL_CONDUCTIVITY] = k
        self.parameters[Parameters.HEAT_SOURCE] = Q
    
    def setSizes(self, nNodes, nElemts, nDirich, nNeumn):
        self.sizes[Sizes.NODES] = nNodes
        self.sizes[Sizes.ELEMENTS] = nElemts
        self.sizes[Sizes.DIRICHLET] = nDirich
        self.sizes[Sizes.NEUMANN] = nNeumn

    def getSize(self, s):
        return self.sizes[s]

    def getParameter(self, p):
        return self.sizes[p]

    ##SI REVIENTA ES POR ESTE METODO
    def createData(self):
        self.node_list =  [self.sizes[Sizes.NODES]]
        self.element_list = [self.sizes[Sizes.ELEMENTS]]
        self.dirichlet_list = [self.sizes[Sizes.DIRICHLET]]
        self.neumann_list = [self.sizes[Sizes.NEUMANN]]

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
        if(type == Sizes.DIRICHLET):
            return self.dirichlet_list[i]
        else:
            return self.neumann_list[i]