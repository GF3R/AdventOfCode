f = open("Input2.txt")
ranges = f.read().split(',')

# function to Check wether its a valid Id
def isValidIdPartOne(id: str):
    return id[0] == 0 or not hasPatternTwice(id)

def isValidIdPartTwo(id: str):
    return not hasRepeatingPattern(id)

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

def hasPatternTwice(id: str):
    if len(id) % 2:
        return False
    
    patternLength = int(len(id)/ 2)
    return id[:patternLength] == id[patternLength:(patternLength+patternLength)]
      
invalidId = 0
# iterate through each range
for range in ranges:
    rangeMinAndMax = range.split('-')
    rangeMin = rangeMinAndMax[0]
    rangeMax = rangeMinAndMax[1]
    fromAsNumber = int(rangeMin)
    toAsNumber = int(rangeMax)
    while fromAsNumber <= toAsNumber:
        if not isValidIdPartTwo(str(fromAsNumber)):
            print("adding number: ", fromAsNumber)
            invalidId += fromAsNumber
        fromAsNumber += 1
    
print(invalidId)