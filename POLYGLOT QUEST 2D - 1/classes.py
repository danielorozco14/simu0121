from enum import Enum

## enums
class Indicators (Enum):
    NOTHING = 0

Lines = Enum('Lines', 'NOLINE SINGLELINE DOUBLELINE')
Modes = Enum ('Modes', 'NOMODE INT_FLOAT INT_FLOAT_FLOAT INT_INT_INT_INT')
Parameters = Enum('Parameters', 'THERMAL_CONDUCTIVITY HEAT_SOURCE')
Sizes = Enum('Sizes', 'NODES ELEMENTS DIRICHLET NEUMANN')
###

class item:
    id = 0
    x = 0.0
    y = 0.0
    node1 = 0
    node2 = 0
    node3 = 0
    value = 0.0

    def setId(self, id):
        self.id = id

    def setX(self, x_coordinate):
        self.x = x_coordinate

    def setY(self, y_coordinate):
        self.y = y_coordinate
    
    def setNode1(self, node1):
        self.node1 = node1
    
    def setNode2(self, node2):
        self.node2 = node2
    
    def setNode3(self, node3):
        self.node3 = node3

    def setValue(self, value):
        self.value = value

    def getId(self):
        return self.id
    
    def getX(self):
        return self.x
    
    def getY(self):
        return self.y
    
    def getNode1(self):
        return self.node1
    
    def getNode2(self):
        return self.node2
    
    def getNode3(self):
        return self.node3
    
    def getValue(self):
        return self.value
    



class Node (item):

    def setValues(self, a, b, c):
        self.id = a
        self.x = b
        self.y = c



class Element(item):
    
   def setValues(self, a, d, e, f):
       self.id = a
       self.node1 = d
       self.node2 = e
       self.node3 = f

class Condition(item):
    
    def setValues(self, d, g):
        self.node1 = d
        self.value = g

class Mesh:
    parameters = []
    sizes = []
    node_list = []
    element_list = []
    indices_dirich = []

    dirichlet_list = [] #Array containing Dirichlet objects
    neumann_list = [] #Array containing Neumann objects

    def setParameters(self, k, Q):
        self.parameters.insert(Parameters.THERMAL_CONDUCTIVITY.value - 1, k)
        self.parameters.insert(Parameters.HEAT_SOURCE.value - 1, Q)
    
    def setSizes(self, nNodes, nElemts, nDirich, nNeumn):
      
        self.sizes.insert(0, nNodes)
        self.sizes.insert(1, nElemts)
        self.sizes.insert(2, nDirich)
        self.sizes.insert(3, nNeumn)
     

    def getSize(self, s):
        return self.sizes[s]

    def getParameter(self, p):
        return self.parameters[p]

    def createData(self):
        
        for i in range(self.sizes[0]):
            obj1 = Node()
            self.node_list.append(obj1)# * self.sizes[0]
        
        for i in range(self.sizes[1]):
            obj2 = Element()
            self.element_list.append(obj2)# * self.sizes[0]
        
        for i in range(self.sizes[2]):
            obj3 = Condition()
            self.dirichlet_list.append(obj3)# * self.sizes[0]

        for i in range(self.sizes[3]):
            obj4 = Condition()
            self.neumann_list.append(obj4)# * self.sizes[0]

        self.indices_dirich = [0] * self.sizes[2] #DIRICH SIZE INDEX = 2 IN sizes Array

        

    def getNodes(self):
        return self.node_list
    
    def getElements(self):
        return self.element_list

    def getDirichletIndices(self):
        return self.indices_dirich

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