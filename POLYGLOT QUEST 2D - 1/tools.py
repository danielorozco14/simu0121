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
                item.setValues(e, r)
               

                if(limit == 0):
                    index = 0
                    break           
                index += 1

        if(mode == classes.Modes.INT_FLOAT_FLOAT and limit > 0):
            row = f.split()
            if(len(row) > 1):
                
                e = int(row[0])
                r = float(row[1])
                rr = float(row[2])
                
                limit -= 1
                item = item_list[index]
                item.setValues(e, r, rr)
               

                if(limit == 0):
                    index = 0
                    break           
                index += 1
    
        if(mode == classes.Modes.INT_INT_INT_INT and limit > 0):

            row = f.split()
            if(len(row) > 1):
                e1 = int(row[0])
                e2 = int(row[1])
                e3 = int(row[2])
                e4 = int(row[3])
                limit -= 1
                item_list[index].setValues(e1, e2, e3, e4)
                
                if(limit == 0):
                    index = 0
                    break  
                index += 1


def correctConditions(n, condition_list, indexs):
    for i in range(n):
        indexs[i] = condition_list[i].getNode1()
    
    for i in range(n-1):
        pivot = condition_list[i].getNode1()
        for j in range(i, n):
            if(condition_list[j].getNode1() > pivot):
                condition_list[j].setNode1(condition_list[j].getNode1() - 1)

def addExtension(new_file_name, file_name, extension):
    ori_length = len(file_name)
    ext_length = len(extension)

    i = 0

    for i in range(ori_length):
        new_file_name[i] = file_name[i]

    for i in range(ext_length):
        new_file_name[ori_length + i] = extension[i]

    new_file_name[ori_length + i] = '\0'

def leerMallayCondiciones(Mesh, filename):
    file_to_open = ""
    
    k = 0.0
    Q = 0.0
    
    nNodes = 0
    nElemts = 0
    nDirich = 0
    nNeumn = 0
    flag = True
   
    #addExtension(file_to_open, filename, ".dat")
    file_to_open = input("Ingrese el nombre del archivo con extension .dat: ")
    while(flag):
        try:            
            file = open(file_to_open)
            if(file):
                cont = 0
                for line in file:
                    file_line = line.split()                  

                    if(cont == 0):
                        k = float(file_line[0])
                        Q = float(file_line[1])

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

        Mesh.setParameters(k,Q)
                
        Mesh.setSizes(nNodes, nElemts, nDirich, nNeumn)
        Mesh.createData()

        
        obtenerDatos(file, classes.Lines.SINGLELINE, nNodes, classes.Modes.INT_FLOAT_FLOAT, Mesh.getNodes())        
        obtenerDatos(file, classes.Lines.DOUBLELINE, nElemts,classes.Modes.INT_INT_INT_INT, Mesh.getElements())
        
        obtenerDatos(file, classes.Lines.DOUBLELINE, nDirich, classes.Modes.INT_FLOAT, Mesh.getDirichlet())
        obtenerDatos(file, classes.Lines.DOUBLELINE, nNeumn, classes.Modes.INT_FLOAT, Mesh.getNeumann())

        file.close()

        correctConditions(nDirich, Mesh.getDirichlet(), Mesh.getDirichletIndices())

def findIndex(v, s, arr):
    for i in range(s):
        if(arr[i] == v):
            return True
    return False

def writeResults(mesh, vector_T, filename):
    output_file_name ="outputMEF2D.post.res"
    
    dirich_indices = mesh.getDirichletIndices()
    dirich_condition = mesh.getDirichlet()

    #addExtension(output_file_name, filename, ".post.res")

    file = open(output_file_name, 'x')

    file.write("GiD Post Results File 1.0\n")
    file.write("Result \"Temperature\" \"Load Case 1\" 1 Scalar OnNodes\nComponentNames \"T\"\nValues\n")

    Tpos = 0
    Dpos = 0

    # size[0] = NODE SIZE
    n = mesh.getSize(0)

    # size[2] = DIRICH SIZE
    nd = mesh.getSize(2)

    for i in range(n):
        if(findIndex(i + 1, nd, dirich_indices)):
            file.write(str(i + 1) + " " + str(dirich_condition[Dpos].getValue()) + "\n")
            Dpos += 1
        else:
            file.write(str(i + 1) + " " + str(vector_T[Tpos]) + "\n")
            Tpos += 1
    
    file.write("End Values \n")
    file.close()


