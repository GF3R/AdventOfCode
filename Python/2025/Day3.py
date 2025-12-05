# input:
#987654321111111
#811111111111119
#234234234234278
#818181911112111
f = open("Input3.txt")
batteryLines = f.read().split('\n')
joltageSum = 0

#part 1
for line in batteryLines :
    numbers = list(map(int, line))
    maxNumber = max(numbers[:-1])
    indexOfMaxNumber = numbers.index(maxNumber)
    maxBehindMax = max(numbers[indexOfMaxNumber+1:])
    joltage = int(str(maxNumber) + str(maxBehindMax))
    joltageSum += joltage

def getMaxNumber(numberLine, index, pos) :
    # 234234234234278
    
    return maxNumber

sumOfOverload = 0
#part 2 
for line in batteryLines :
    numbers = list(map(int, line))
    maxNumber = max(numbers[:-12])
    maxLineNumber = ""
    lastMaxIndex = 0
    currentNeededReaminingLength = 11
    while len(maxLineNumber) < 12:
        if len(numbers) > 1 and currentNeededReaminingLength > 0 :
            selectableNumbers = numbers[:-currentNeededReaminingLength]
        else :
            selectableNumbers = numbers
        maxNumber = max(selectableNumbers)
        index = numbers.index(maxNumber)
        numbers = numbers[index+1:]
        maxLineNumber += str(maxNumber)
        currentNeededReaminingLength -= 1
    # for number in range(12, 0, -1):
    #     maxNumber = getMaxNumber(numbers, lastMaxIndex, number)
    #     maxLineNumber += str(maxNumber)
    #     lastMaxIndex = numbers.index(maxNumber, lastMaxIndex)+1
    #     print(maxNumber, "",lastMaxIndex)
    print("maxLnumber", maxLineNumber)
    sumOfOverload += int(maxLineNumber)

    
print(joltageSum)
print("part 1", 17430)
print(sumOfOverload)
print("part 2", 0),