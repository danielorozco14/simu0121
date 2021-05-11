import classes

def obtenerDatos(file, nlines, n, mode, item_list):
    #Para saltarse filas
    file.readline()
    #De ser necesario de saltarse mas de un fila
    if(nlines == classes.Lines.DOUBLELINE):
        file.readline()

    e = 0
    r = 0.0
    index = 0
    for f in file:

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


                

'''
Mesh = classes.Mesh()
file = open("problem.msh")
obtenerDatos(file, classes.Lines.DOUBLELINE, 10, classes.Modes.INT_INT_INT, Mesh.getElements())
cont = 0

for row in file:
    value = row.split()
    print(value)
    cont+=1
    if cont == 10:
        break
print(file.readline())
'''