f = open("Input7.txt")
lines = f.read().split('\n')

xRange = len(lines)
yRange = len(lines[0])
start = ()
laserMap = {}

def printMap(mapAsDict: dict) :
    lines = ['' for _ in range(xRange)]
    for x in range(0, xRange) :
        for y in range(0, yRange) :
            lines[x] += mapAsDict[(x,y)]

    for line in lines:
        print(line)
        
for x, line in enumerate(lines) :
    for y, char in enumerate(list(line)) :
        laserMap[(x,y)] = char
        if char == 'S':
            start = (x,y)

lasers = [start]
newMap = laserMap.copy()
splits = 0
while True :
    newLasers = []
    if len(lasers) <= 0:
        break

    for laser in lasers :
        x, y = laser
        # try to move forward
        if x >= len(lines) -1 :
            continue
        
        nextStep = newMap[x+1,y]
        if nextStep == '.' :
            newLasers.append((x+1,y))
            newMap[(x+1,y)] = '|'
        elif nextStep == '^' :
            hasSplit = False
            if newMap[(x+1,y+1)] == '.':
                newLasers.append((x+1,y+1))
                newMap[(x+1,y+1)] = '|'
                hasSplit = True
            if newMap[(x+1,y-1)] == '.':
                newLasers.append((x+1,y-1))
                newMap[(x+1,y-1)] = '|'
                hasSplit = True
            if hasSplit :
                splits += 1
    lasers = newLasers

printMap(laserMap)
print("--------------------------------")
printMap(newMap)
print(splits)

knownTimelines = {}

def GetTimelinesForFork(laserOrigin, mapOnSplit, currentNumberOfTimelines): 
    x, y = laserOrigin
    if laserOrigin in knownTimelines :
        return knownTimelines[laserOrigin]
    # try to move forward
    if x >= xRange -1 :
        knownTimelines[laserOrigin] = currentNumberOfTimelines +1
        return currentNumberOfTimelines +1
    nextStep = mapOnSplit[x+1,y]
    while nextStep == '.' :
        mapOnSplit[(x+1,y)] = '|'
        x += 1
        if x >= xRange -1:
            knownTimelines[laserOrigin] = currentNumberOfTimelines +1
            return currentNumberOfTimelines +1
        nextStep = mapOnSplit[x+1,y]
    forksa = 0
    forksb = 0
    
    if nextStep == '^' :
        cordsa = (x+1,y+1)
 
        if mapOnSplit[cordsa] == '.' :
            copyA = mapOnSplit.copy()
            copyA[cordsa] = '|'
            forksa = GetTimelinesForFork(cordsa, copyA, currentNumberOfTimelines) 
        cordsb = (x+1, y-1)
        if mapOnSplit[cordsb] == '.' :
            copyB = mapOnSplit.copy()
            copyB[cordsb] = '|'
            forksb =  GetTimelinesForFork(cordsb, copyB, currentNumberOfTimelines) 
    knownTimelines[laserOrigin] = forksa + forksb
    return forksa + forksb

timelines = GetTimelinesForFork(start, laserMap.copy(), 0)
print(timelines)
print(40)