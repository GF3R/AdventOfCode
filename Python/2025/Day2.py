f = open("Input2.txt")
ranges = f.read().split(',')

# part two
def hasRepeatingPattern(id: str):    
    if len(id) <= 1 :
        return False

    idLength = len(id)
    for pat_len in range(1, idLength // 2 + 1):
        if idLength % pat_len:  # pattern must tile the whole string
            continue
        pat = id[:pat_len]
        if pat * (idLength // pat_len) == id:
            return True
    return False

#part one
def hasPatternTwice(id: int):
    if len(id) % 2:
        return False
    
    patternLength = int(len(id)/ 2)
    return id[:patternLength] == id[patternLength:(patternLength+patternLength)]

partOne = 0
partTwo = 0

for span in ranges:
    start, end = map(int, span.split("-", 1))
    partOne += sum([n for n in range(start, end + 1) if hasPatternTwice(str(n))])
    partTwo += sum([n for n in range(start, end + 1) if hasRepeatingPattern(str(n))])
    
print("part one:", partOne, "part two:", partTwo)
print("correct answer Part one:", 15873079081, "correct answer Part two:", 22617871034)
isWorking = (partOne == 15873079081) and (partTwo == 22617871034)
print("is working:", isWorking)