import sys
import sel

def Zeroes(matrix, n):
    for i in range(0, n):
        row = [0.0] * n
        matrix.append(row)

#OJO
def Zeroes3(matrix, n, m):
    for i in range(n):
        row = [0.0] * m
        matrix.append(row)

def zeroes(vector, n):
    for i in range(0, n):
        vector.append(0.0)



def copyMatrix(matrix, copy):
    Zeroes(copy, len(matrix))
    for i in range(0, len(matrix)):
        for j in range(0, len(matrix[0])):
            copy[i][j] = matrix[i][j]

def calculateMember(i, j, r, matrix_A, matrix_B):

    member = 0
    for k in range(r):
        member += matrix_A[i][k] * matrix_B[k][j]
    
    return member

def productMatrixMatrix(matrix_A, matrix_B, n, r, m):
    R = []
    Zeroes3(R, n, m)

    for i in range(n):
        for j in range(m):
            R[i][j] = calculateMember(i, j, r, matrix_A, matrix_B)
    
    return R

def productMatrixVector(matrix, vector, result):
    
    for fila in range(0, len(matrix)):
        cell = 0.0
        for celda in range(0, len(vector)):
            cell += matrix[fila][celda] * vector[celda]

        result[fila] += cell
    

def productRealMatrix(real, matrix, result):
    Zeroes(result, len(matrix))
    for i in range(0, len(matrix)):
        for j in range(0, len(matrix[0])):
            result[i][j] = real * matrix[i][j]

def getMinor(matrix, i, j):
    del matrix[i] #Si tuesta es por esto
    for i in range(0, len(matrix)):         
       del matrix[i][j]

def determinant(matrix):
    if(len(matrix) == 1):
        return matrix[0][0] 
    else:
        det = 0.0
        for i in range(0, len(matrix[0])):
            minor = []

            copyMatrix(matrix, minor)            
            getMinor(minor, 0, i)

            det += pow(-1, i) * matrix[0][i] * determinant(minor)
    
        return det

def cofactors(matrix, cofactors):
    Zeroes(cofactors, len(matrix))

    for i in range(0, len(matrix)):
        for j in range(0, len(matrix[0])):
            minor = []
            copyMatrix(matrix, minor)
            getMinor(minor, i, j)

            cofactors[i][j] =  pow(-1, i + j) * determinant(minor)

def transpose(matrix, matrix_transpose):
    
    Zeroes3(matrix_transpose, len(matrix[0]), len(matrix))

    for i in range(0, len(matrix)):
        for j in range(0, len(matrix[0])):
            matrix_transpose[j][i] = matrix[i][j]

def inverseMatrix(matrix, inverse):
    Cof = []
    Adj = []

    det = determinant(matrix)
    if(det == 0):
        sys.exit("Matriz no invertible")
        
    cofactors(matrix, Cof)
    transpose(Cof, Adj)
    productRealMatrix(1/det, Adj, inverse)
    