import classes
import math_tools

def showMatrix(kMatrix):
    for i in range(0,len(kMatrix[0]) + 1):
        print('{\t')
        for j in range(0, len(kMatrix) + 1):
            print(kMatrix[i][j] + "\t")
        print("}\n")

def showKs(KsMatrix):
    for i in range(0, len(KsMatrix) + 1):
        print("K del elemento " + (i + 1) + ":\n")
        showMatrix(KsMatrix[i])
        print("************************************\n")

def showVector(Vector):
    print("{\t")
    for i in range(0, len(Vector) + 1):
        print(Vector[i] + "\t")
    print("}\n")

def showbs(bs):
    for i in range(0, len(bs) + 1):
        print("b del elemento " + i+1 + ":\n")
        showVector(bs[i])
        print("****************************\n")

# SI PETA, ES PORQUE MESH NO DEBERIA SER CREADO EN LA FUNCION
# SINO PASADO COMO ARGUMENTO
def createLocalK(element, mesh):

    

    K = []
    row1 = []
    row2 = []

    k = mesh.getParameter(classes.Parameters.THERMAL_CONDUCTIVITY)
    l = mesh.getParameter(classes.Parameters.ELEMENT_LENGTH)

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
    
    
    Q = mesh.getParameter(classes.Parameters.HEAT_SOURCE)
    l = mesh.getParameter(classes.Parameters.ELEMENT_LENGTH)
    
    b.append( Q*l/2)
    b.append( Q*l/2)

    return b


# SI PETA, ES PORQUE MESH NO DEBERIA SER CREADO EN LA FUNCION
# SINO PASADO COMO ARGUMENTO
def crearSistemasLocales(mesh, localKs, localBs):

    for i in range(0, mesh.getParameter(classes.Sizes.ELEMENTS)):
        localKs.append(createLocalK(i, mesh))
        localBs.append(createLocalb(i, mesh))

def assemblyK(element, localK, matrixK):
    index1 = element.getNode1() - 1
    index2 = element.getNode2() - 1

    matrixK[index1][index1] += localK[0][0]
    matrixK[index1][index2] += localK[0][1]
    matrixK[index2][index1] += localK[1][0]
    matrixK[index2][index1] += localK[1][1]

def assemblyB(element, localB, vectorB):
    index1 = element.getNode1() - 1
    index2 = element.getNode2() - 1

    vectorB[index1] += localB[0]
    vectorB[index2] += localB[1]

def ensamblaje(mesh, localKs, localBs, K, b):
    for i in range(0, mesh.getSize(classes.Sizes.ELEMENTS) + 1):
        element = mesh.getElement(i)
        assemblyK(element, localKs[i], K)
        assemblyB(element, localBs[i], b)


def applyNeumann(mesh, b):
    for i in range(0, mesh.getSize(classes.Sizes.NEUMANN)):

        condition = mesh.getCondition(i, classes.Sizes.NEUMANN)
        b[condition.getNode1()-1] += condition.getValue()

def applyDirichlet(mesh, K, b):
    for i in range(0, mesh.getSize(classes.Sizes.DIRICHLET) + 1):
        
        condition = mesh.getCondition(i, classes.Sizes.DIRICHLET)
        index = condition.getNode1() - 1

        K.pop(index)
        b.pop(index)

        for row in range(0, len(K) + 1):
            cell = K[row][index]
            K[0].pop(index)
            b[row] += -1 * condition.getValue() * cell

##SI LOS CALCULOS NO PEGAN, PROBABLEMENTE SEA PORQUE LOS ARREGLOS
# SON MATRICES Y NO SOLO []. REVISARA EN TODOS LOS ARCHIVOS

def calculate(K, b, T):
    Kinversa = []

    math_tools.inverseMatrix(K, Kinversa)

    ## Hacer estas funciones en math_tools.py
    math_tools.productMatrixVector(K, b, T)