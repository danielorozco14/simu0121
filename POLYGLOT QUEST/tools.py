import classes

def obtenerDatos(file_name, mode, nLines, item_list):
       
    with open(f"{file_name}") as file:
        for line in file:
            row = line.split()
            try:
                while(nLines > 0):
                    for item in range(0, len(row)):
                        if(len(row) > 1):
                            if(mode == classes.Lines.INT_FLOAT):
                                e = int(row[0])
                                r = float(row[1])
                                item_list[item].sentIntFloat(e,r)

                            if(mode == classes.Lines.INT_INT_INT):
                                e1 = int(row[0])
                                e2 = int(row[1])
                                e3 = int(row[2])
                                item_list[item].sentIntIntInt(e1, e2, e3)
                    nLines -= 1
            except (IndexError):
                print("Index Error occurred")



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