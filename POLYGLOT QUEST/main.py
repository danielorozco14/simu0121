import math_tools
import classes
import tools
import sel


def main():
    #[0] * 1
    localKs = []
    localBs = []

    matrix_K = []
    vector_b = []
    vector_T = []

    print("IMPLEMENTACION DEL METODO DE LOS ELEMENTOS FINITOS EN UNA DIMENSION")
    print("\tTRANSFERENCIA DE CALOR\t- 1 DIMENSIÓN")
    print("\tFUNCIONES DE FORMA LINEALES\t- PESOS DE GALERKIN")

    Mesh = classes.Mesh()
    tools.leerMallayCondiciones(Mesh)

    
    sel.crearSistemasLocales(Mesh, localKs, localBs)
    
    '''#PARA MOSTRAR LAS MATRICES K Y EL VECTOR B
    sel.showKs(localKs)
    sel.showbs(localBs)
    '''

    
    math_tools.Zeroes(matrix_K, Mesh.getSize(classes.Sizes.NODES.value - 1))
    math_tools.zeroes(vector_b, Mesh.getSize(classes.Sizes.NODES.value - 1))
    
    sel.ensamblaje(Mesh, localKs, localBs, matrix_K, vector_b)
    sel.applyNeumann(Mesh, vector_b)
    sel.applyDirichlet(Mesh, matrix_K, vector_b)
    
    math_tools.zeroes(vector_T, len(vector_b))

    sel.calculate(matrix_K, vector_b, vector_T)

    print("La respuesta es: ")
    sel.showVector(vector_T)

main()

