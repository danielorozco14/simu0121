import classes
import math_tools
import math

def showMatrix(kMatrix):
    for i in range(0,len(kMatrix[0])):
        print('[',end=" ")
        for j in range(0, len(kMatrix)):
            print(str(kMatrix[i][j]),end=" " )
        print("]")
        

def showKs(KsMatrix):
    for i in range(0, len(KsMatrix)):
        print("K del elemento " + str(i + 1) + ":\n")
        showMatrix(KsMatrix[i])
        print("************************************\n")

def showVector(Vector):
    print("\n[",end=" ")
    for i in range(0, len(Vector)):
        print(str(Vector[i]), end = " ")
    print("]")

def showbs(bs):
    for i in range(0, len(bs)):
        print("b del elemento " + str(i+1) + ":")
        showVector(bs[i])
        print("\n****************************")


def calculateLocalD(i, mesh):
    D = 0.0
    a = 0.0
    b = 0.0
    c = 0.0 
    d = 0.0

    element = mesh.getElement(i)

    node1 = mesh.getNode(element.getNode1() - 1)
    node2 = mesh.getNode(element.getNode2() - 1)
    node3 = mesh.getNode(element.getNode3() - 1)

    a = node2.getX() - node1.getX()
    b = node2.getY() - node1.getY()
    c = node3.getX() - node1.getX()
    d = node3.getY() - node1.getY()

    D = (a * d) - (b * c)

    return D


def calculateMagnitude(v1, v2):
    return math.sqrt(pow(v1, 2) + pow(v2, 2))

def calculateLocalArea(i, mesh):
    #Formula de Heron

    A = 0.00
    s = 0.00
    a = 0.00
    b = 0.00
    c = 0.00

    element = mesh.getElement(i)
    
    node1 = mesh.getNode(element.getNode1() - 1)
    node2 = mesh.getNode(element.getNode2() - 1)
    node3 = mesh.getNode(element.getNode3() - 1)

    a = calculateMagnitude(node2.getX() - node1.getX(), node2.getY() - node1.getY())
    b = calculateMagnitude(node3.getX() - node2.getX(), node3.getY() - node2.getY())
    c = calculateMagnitude(node3.getX() - node1.getX(), node3.getY() - node1.getY())
    s = (a + b + c) / 2

    A = math.sqrt(s * (s - a) * (s - b) * (s - c))

    return A

def calculateLocalA(i, matrix_A, mesh):
    
    element = mesh.getElement(i)
    
    node1 = mesh.getNode(element.getNode1() - 1)
    node2 = mesh.getNode(element.getNode2() - 1)
    node3 = mesh.getNode(element.getNode3() - 1)

    matrix_A[0][0] = node3.getY() - node1.getY()
    matrix_A[0][1] = node1.getY() - node2.getY()

    matrix_A[1][0] = node1.getX() - node3.getX() 
    matrix_A[1][1] = node2.getX() - node1.getX()

def calculateB(matrix_B):
    matrix_B[0][0] = -1
    matrix_B[1][0] = -1

    matrix_B[0][1] = 1
    matrix_B[1][1] = 0

    matrix_B[0][2] = 0
    matrix_B[1][2] = 1


def createLocalK(element, mesh):    

    D = mesh.getParameter(classes.Parameters.THERMAL_CONDUCTIVITY.value - 1)
    Ae = mesh.getParameter(classes.Parameters.THERMAL_CONDUCTIVITY.value - 1)
    k = mesh.getParameter(classes.Parameters.THERMAL_CONDUCTIVITY.value - 1)


    K = []
    A = []
    B = []
    Bt = []
    At = []

    D = calculateLocalD(element, mesh)
    Ae = calculateLocalArea(element, mesh)

    math_tools.Zeroes(A, 2)
    math_tools.Zeroes3(B, 2, 3)

    calculateLocalA(element, A, mesh)
    calculateB(B)

    math_tools.transpose(A, At)
    math_tools.transpose(B, Bt)

    math_tools.productRealMatrix(k * Ae / (D * D), 
    math_tools.productMatrixMatrix(Bt, math_tools.productMatrixMatrix(At, math_tools.productMatrixMatrix(A,B,2,2,3),2,2,3),3,2,3),K)
    
    return K


def calculateLocalJ(i, mesh):
    J= 0.00
    a= 0.00 
    b= 0.00 
    c= 0.00 
    d = 0.00

    element = mesh.getElement(i)

    # LOOK UP FOR THIS IF CRASH
    node1 = mesh.getNode(element.getNode1() - 1)
    node2 = mesh.getNode(element.getNode2() - 1)
    node3 = mesh.getNode(element.getNode3() - 1)

    a = node2.getX() - node1.getX()
    b = node3.getX() - node1.getX()
    c = node2.getY() - node1.getY()
    d = node3.getY() - node1.getY()

    J = (a * d) - (b * c)

    return J



def createLocalb(element, mesh):
    b = []
        
    Q = mesh.getParameter(classes.Parameters.HEAT_SOURCE.value - 1)
    J = calculateLocalJ(element, mesh)
    b_i = Q * J /6
  
    b.append(b_i)
    b.append(b_i)
    b.append(b_i)  

    return b



def crearSistemasLocales(mesh, localKs, localBs):
    
    for i in range(0, mesh.getSize(1)):
        localKs.append(createLocalK(i, mesh))
        localBs.append(createLocalb(i, mesh))
    

def assemblyK(element, localK, matrixK):
    index1 = element.getNode1() - 1
    index2 = element.getNode2() - 1
    index3 = element.getNode3() - 1
 
    matrixK[index1][index1] += localK[0][0]
    matrixK[index1][index2] += localK[0][1]
    matrixK[index1][index3] += localK[0][2]

    matrixK[index2][index1] += localK[1][0]
    matrixK[index2][index2] += localK[1][1]
    matrixK[index2][index3] += localK[1][2]

    matrixK[index3][index1] += localK[2][0]
    matrixK[index3][index2] += localK[2][1]
    matrixK[index3][index3] += localK[2][2]
    
def assemblyB(element, localB, vectorB):
    index1 = element.getNode1() - 1
    index2 = element.getNode2() - 1
    index3 = element.getNode3() - 1


    vectorB[index1] += localB[0]
    vectorB[index2] += localB[1]
    vectorB[index3] += localB[2]

def ensamblaje(mesh, localKs, localBs, K, b):
    #ELEMENTS = 1
    for i in range(0, mesh.getSize(1)):
        element = mesh.getElement(i)
        assemblyK(element, localKs[i], K)
        assemblyB(element, localBs[i], b)


def applyNeumann(mesh, b):
    # sizes[3] = cantidad de condiciones de neumann
    for i in range(0, mesh.getSize(3)):
        condition = mesh.getCondition(i, classes.Sizes.NEUMANN)
        b[condition.getNode1()-1] += condition.getValue()


def applyDirichlet(mesh, K, b):
    # sizes[2] = cantidad de condiciones de dirichlet
    for i in range(0, mesh.getSize(2)):
        
        condition = mesh.getCondition(i, classes.Sizes.DIRICHLET)
        index = condition.getNode1() - 1
       
        K.pop(index)
        
        b.pop(index)
        
        for row in range(0, len(K)):
            cell = K[row][index]
            K[row].pop(index)
            b[row] = b[row] + (-1 * condition.getValue() * cell)
    

def calculate(K, b, T):
    Kinversa = []

    math_tools.inverseMatrix(K, Kinversa)

    ## Hacer estas funciones en math_tools.py
    math_tools.productMatrixVector(Kinversa, b, T)
   
