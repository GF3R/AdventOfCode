f = open("Input5.txt")
lines = f.read().split('\n\n')
freshRows = lines[0].split('\n')
ingredients = lines[1].split('\n')
freshNumbers = []

# for row in freshRows :
#     von, bis = row.split('-',1)
#     freshNumbers.extend(list(range(int(von), int(bis)+1)))
    
# isFresh = 0
# for number in ingredients :
#     if int(number) in freshNumbers:
#         isFresh += 1
#         print(number)

def rowToRange(input):
    von, bis = list(map(int, input.split('-',1)))
    return  [von, bis]

    
numbers = list(map(int,ingredients))
rows = list(map(rowToRange, freshRows))

# part one
isFresh = 0
for row in rows:
    numbersInRange = [x for x in numbers if row[0] <= x <= row[1]]
    for number in numbersInRange :
        numbers.remove(number)
    isFresh += len(numbersInRange)

print(isFresh)

# part two
def any_overlap(ranges):
    if not ranges:
        return False

    intervals = sorted((min(a, b), max(a, b)) for a, b in ranges)

    prev_start, prev_end = intervals[0]

    for start, end in intervals[1:]:
        if start <= prev_end:
            return True
        prev_end = max(prev_end, end)

    return False
def overlaps(a, b):
    (s1, e1), (s2, e2) = a, b
    return max(s1, s2) <= min(e1, e2)

def merge(listOfRows : list) :
    mergedRows = []
    listOfRows = sorted(listOfRows, key=lambda list: list[0])
    current_start, current_end = listOfRows[0]

    for start, end in listOfRows[1:]:
        if start <= current_end:
            current_end = max(current_end, end)
        else:
            mergedRows.append((current_start, current_end))
            current_start, current_end = start, end

    mergedRows.append((current_start, current_end))
    return mergedRows

newRows = merge(rows)
print("merge", newRows) 
print(any_overlap(newRows))

sumOfIntervals = 0
for interval in newRows :
    sumOfIntervals += interval[1]+1 - interval[0]
    
print(sumOfIntervals)
print("334714395325710")

