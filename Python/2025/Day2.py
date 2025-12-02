f = open("Input2.txt")
ranges = f.read().split(',')

# part two
def hasRepeatingPattern(id: str):    
    if len(id) <= 1 :
        return False

    patternLength = 1
    hasPattern = True
    
    while patternLength < len(id):
        pattern = id[:patternLength]
        indexInId = 0
        hasPattern = True
        
        while indexInId < len(id):
            if pattern != id[indexInId:(indexInId+patternLength)]:
                hasPattern = False
                break
            indexInId += patternLength
            
        if hasPattern:
            patternLength = len(id)  # to break outer loop
        
        patternLength += 1
    return hasPattern

#part one
def hasPatternTwice(id: int):
    if len(id) % 2:
        return False
    
    patternLength = int(len(id)/ 2)
    return id[:patternLength] == id[patternLength:(patternLength+patternLength)]
      
invalidId = 0
# iterate through each range
for range in ranges:
    rangeMinAndMax = range.split('-')
    fromAsNumber = int(rangeMinAndMax[0])
    toAsNumber = int(rangeMinAndMax[1])
    while fromAsNumber <= toAsNumber:
        if not hasRepeatingPattern(str(fromAsNumber)):
            print("adding number: ", fromAsNumber)
            invalidId += fromAsNumber
        fromAsNumber += 1
    
print(invalidId)