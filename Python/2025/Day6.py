f = open("Input6.txt")
lines = f.read().split('\n')
dictionary = {}
        
class Calculation :
    def __init__(self, index):
        self.values = []
        self.operator = ''
        self.index = index
        
    def calculate(self) :
        if self.operator == '*':
            result = 1
            for value in self.values :
                result *= value
            return result
        elif self.operator == '+' :
            return sum(self.values)
        
            
        
for line in lines :
    cleaned = [s.strip() for s in line.split(' ') if isinstance(s, str) and s.strip()]
    for pos, character in enumerate(cleaned) :
        if pos not in dictionary :
            dictionary[pos] = Calculation(pos)
        if character.isnumeric() :
            dictionary[pos].values.append(int(character))
        else :
            dictionary[pos].operator = character

totalSum = 0
for elem in dictionary :
    totalSum += dictionary[elem].calculate()
    
print(totalSum)
        
# part 2
dictionary = {}
        
class CephaCalculation :
    def __init__(self, index):
        self.values = { 0: "", 1: "", 2: "", 3: ""}
        self.operator = ''
        self.index = index
        
    def calculate(self) :
        if self.operator == '*':
            result = 1
            for value in self.values :
                if(self.values[value]):
                    result *= int(self.values[value])
            return result
        elif self.operator == '+' :
            return sum(self.values)
        
            
rows = {}
operators = {}
print("part 2")
for line in lines :
    for index, char in enumerate(list(line)) :
        if char.isnumeric() :
            if index not in rows :
                rows[index] = ''
            rows[index] += char
        elif char.strip() :
            operators[index] = char.strip()
sumPerOperator = {}
index = 0
operators = list(operators.values())

print(operators)
previousKey = 0
operatorIndex = 0
rows = dict(sorted(rows.items()))
print(rows)

for row in rows :
    if row > previousKey+1 :
        operatorIndex += 1
    previousKey = row
    currentOp = operators[operatorIndex]
    isMult = currentOp == '*'
    
    if operatorIndex not in sumPerOperator :
        if isMult :
            sumPerOperator[operatorIndex] = 1
        else :
            sumPerOperator[operatorIndex] = 0
    
    if isMult :
        sumPerOperator[operatorIndex] *= int(rows[row])
    else :
        sumPerOperator[operatorIndex] += int(rows[row])
    
print("sum", sumPerOperator)    
totalSum = 0
for operatorSum in sumPerOperator :
    totalSum += sumPerOperator[operatorSum]
    
print(totalSum)
