f = open("Input4.txt")
toiletPapers = f.readlines()

grid1 = [[0 for i in range(len(toiletPapers))] for i in range(len(toiletPapers[0]))]
grid2 = [[0 for i in range(len(toiletPapers))] for i in range(len(toiletPapers[0]))]
rowLen = len(toiletPapers[0])
columnLen = len(toiletPapers)

def isTP(i, j):
    if i >= columnLen or j >= rowLen-1 or i < 0 or j < 0 :
        return 0
    
    if grid1[i][j] == '@':
        return 1
    return 0
        
def howManyNeighboursToiletpaper(i, j):
    toiletPapers = 0
    toiletPapers += isTP(i-1,j)
    toiletPapers += isTP(i-1,j-1)
    toiletPapers += isTP(i-1,j+1)
    toiletPapers += isTP(i, j-1)
    toiletPapers += isTP(i, j+1)
    toiletPapers += isTP(i+1,j)
    toiletPapers += isTP(i+1,j-1)
    toiletPapers += isTP(i+1,j+1)
    return toiletPapers

for line in grid1 :
    print(line)
    
# migrate To Grid
for i in range(len(grid1)-1):
    for j in range(len(grid1[0])):
        grid1[i][j] = toiletPapers[i][j]
        grid2[i][j] = toiletPapers[i][j]

toMove = 0
for i in range(len(grid1)-1):
    for j in range(len(grid1[0])):
        if toiletPapers[i][j] == '@':
            neighbours = howManyNeighboursToiletpaper(i, j)
            if neighbours < 4 :
                grid2[i][j] = 'X'
                toMove += 1

            
for line in grid1 :
    print(line)
    
print("part 1", toMove)

progress = 1
while progress > 0 :
    print("again")
    progress = 0
    #migrate grid
    for i in range(len(grid1)-1):
        for j in range(len(grid1[0])):
            grid1[i][j] = grid2[i][j]
    for i in range(len(grid1)-1):
        for j in range(len(grid1[0])):
            if grid1[i][j] == '@':
                neighbours = howManyNeighboursToiletpaper(i, j)
                if neighbours < 4 :
                    grid2[i][j] = 'X'
                    toMove += 1
                    progress += 1
                    
for line in grid2 :
    print(line)
print("part 2", toMove)