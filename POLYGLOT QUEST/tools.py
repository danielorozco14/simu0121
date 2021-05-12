import classes

def obtenerDatos(file, nlines, n, mode, item_list):
    #Para saltarse filas
    file.readline()
    #De ser necesario de saltarse mas de un fila
    if(nlines == classes.Lines.DOUBLELINE):
        file.readline()
    index = 0
    limit = n
    for f in file:        
        if(mode == classes.Modes.INT_FLOAT and limit > 0):
            row = f.split()
            if(len(row) > 1):
                
                e = int(row[0])
                r = float(row[1])
                
                limit -= 1
                item = item_list[index]
                item.sentIntFloat(e, r)
               

                if(limit == 0):
                    index = 0
                    break           
                index += 1
    
        if(mode == classes.Modes.INT_INT_INT and limit > 0):

            row = f.split()
            if(len(row) > 1):
                e1 = int(row[0])
                e2 = int(row[1])
                e3 = int(row[2])
                limit -= 1
                item_list[index].sentIntIntInt(e1, e2, e3)
                
                if(limit == 0):
                    index = 0
                    break  
                index += 1




def leerMallayCondiciones(Mesh):
    file = " "
    l = 0.0
    k = 0.0
    Q = 0.0
    nNodes = 0
    nElemts = 0
    nDirich = 0
    nNeumn = 0
    flag = True
   
    while(flag):
        try:
            filename = input("Ingrese el nombre del archivo que contiene los datos de la malla:\n")
            file = open(filename)
            if(file):
                cont = 0
                for line in file:
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
                flag = False

        except (FileNotFoundError):
            print("El archivo no ha sido encontrado")

        Mesh.setParameters(l,k,Q)
                
        Mesh.setSizes(nNodes, nElemts, nDirich, nNeumn)
        Mesh.createData()

        
        obtenerDatos(file, classes.Lines.SINGLELINE, nNodes, classes.Modes.INT_FLOAT, Mesh.getNodes())        
        obtenerDatos(file, classes.Lines.DOUBLELINE, nElemts,classes.Modes.INT_INT_INT, Mesh.getElements())
        
        obtenerDatos(file, classes.Lines.DOUBLELINE, nDirich, classes.Modes.INT_FLOAT, Mesh.getDirichlet())
        obtenerDatos(file, classes.Lines.DOUBLELINE, nNeumn, classes.Modes.INT_FLOAT, Mesh.getNeumann())

        file.close()
