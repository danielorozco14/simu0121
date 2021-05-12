import classes
import math_tools

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

# SI PETA, ES PORQUE MESH NO DEBERIA SER CREADO EN LA FUNCION
# SINO PASADO COMO ARGUMENTO
def createLocalK(element, mesh):    

    K = []
    row1 = []
    row2 = []

    k = mesh.getParameter(classes.Parameters.THERMAL_CONDUCTIVITY.value - 1)
    l = mesh.getParameter(classes.Parameters.ELEMENT_LENGTH.value - 1)
    
    row1.append(k/l)
    row1.append(-k/l)

    row2.append(-k/l)
    row2.append(k/l)

    K.append(row1)
    K.append(row2)
    

    return K


# SI PETA, ES PORQUE MESH NO DEBERIA SER CREADO EN LA FUNCION
# SINO PASADO COMO ARGUMENTO
def createLocalb(element, mesh):
    b = []
    
    
    Q = mesh.getParameter(classes.Parameters.HEAT_SOURCE.value - 1)
    l = mesh.getParameter(classes.Parameters.ELEMENT_LENGTH.value - 1)
    
    b.append( Q*l/2)
    b.append( Q*l/2)

    return b


# SI PETA, ES PORQUE MESH NO DEBERIA SER CREADO EN LA FUNCION
# SINO PASADO COMO ARGUMENTO
def crearSistemasLocales(mesh, localKs, localBs):
    
    for i in range(0, mesh.getSize(1)):
        localKs.append(createLocalK(i, mesh))
        localBs.append(createLocalb(i, mesh))
    

def assemblyK(element, localK, matrixK):
    index1 = element.getNode1() - 1
    index2 = element.getNode2() - 1
 
    matrixK[index1][index1] += localK[0][0]
    matrixK[index1][index2] += localK[0][1]
    matrixK[index2][index1] += localK[1][0]
    matrixK[index2][index2] += localK[1][1]
    
def assemblyB(element, localB, vectorB):
    index1 = element.getNode1() - 1
    index2 = element.getNode2() - 1

    vectorB[index1] += localB[0]
    vectorB[index2] += localB[1]

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
   