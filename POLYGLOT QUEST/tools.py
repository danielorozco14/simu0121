import classes

def obtenerDatos(file, nlines, n, mode, item_list):
    #Para saltarse filas
    file.readline()
    #De ser necesario de saltarse mas de un fila
    if(nlines == classes.Lines.DOUBLELINE):
        file.readline()

    index = 0
    for f in file:
        try:
            if(mode == classes.Modes.INT_FLOAT and n > 0):
                row = f.split()
                if(len(row) > 1):
                    e = int(row[0])
                    r = float(row[1])
                    n -= 1
                    item_list[index].setIntFloat(e, r)
                    index += 1

            if(mode == classes.Modes.INT_INT_INT and n > 0):
                row = f.split()
                if(len(row) > 1):
                    print(row)
                    e1 = int(row[0])
                    e2 = int(row[1])
                    e3 = int(row[2])
                    n -= 1
                    item_list[index].setIntIntInt(e1, e2, e3)
                    index += 1
        except(IndexError):
            print("Index error ocurred at obtenerDatos()\n")




def leerMallayCondiciones(Mesh):
    filename = ""
    flag = True

    l = 0.0
    k = 0.0
    Q = 0.0

    nNodes = 0
    nElemts = 0 
    nDirich = 0
    nNeumn = 0
    
    while(flag):
        try:
            filename = input("Ingrese el nombre del archivo que contiene los datos de la malla:\n")
            f = open(filename)
            if(f):
                cont = 0
                for line in f:
                    file_line = line.split()                  

                    if(cont == 0):
                        l = float(file_line[0])
                        k = float(file_line[1])
                        Q = float(file_line[2])
                    if(cont == 1):
                        nNodes = int(file_line[0])
                        nElemts = int(file_line[1])
                        nDirich = int(file_line[2])
                        nNeumn = int(file_line[3])
                    if cont > 1:
                        break       
                    cont += 1             

                Mesh.setParameters(l,k,Q)
                Mesh.setSizes(nNodes, nElemts, nDirich, nNeumn)
                Mesh.createData()

                obtenerDatos(filename,classes.Lines.SINGLELINE,Mesh.getNodes() )
                obtenerDatos(filename, classes.Lines.DOUBLELINE, )




                f.close()
                flag = False
        except (FileNotFoundError):
            print("El archivo no ha sido encontrado")

Mesh = classes.Mesh()
leerMallayCondiciones(Mesh)