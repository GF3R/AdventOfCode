f = open("Input11.txt")
lines = f.read().split('\n')
fromTo = {}


for line in lines : 
    fromLine, toUnsplitted = line.split(':', 1)
    toSplitted = toUnsplitted.strip().split(' ')
    
    if fromLine not in fromTo :
        fromTo[fromLine] = []
        
    fromTo[fromLine].extend(toSplitted)

def printDict(dictionary):
    for element in dictionary:
        print(element, " ", dictionary[element])

paths = []
def findPaths(dictFromTo, startingPoint, currentPath):
    startingSet = dictFromTo[startingPoint]
    for starting in startingSet:        
        if starting == "out":
            paths.append(currentPath + "," + starting)
            return currentPath + "," + starting
        findPaths(dictFromTo, starting, currentPath + "," + starting)

def count_paths_with_both(graph, start, end, must_visit=("dac", "fft")):
    memo = {}

    def dfs(node, visited):
        if node in must_visit:
            visited += 1

        key = (node, visited)
        
        if key in memo:
            return memo[key]

        if node == end:
            if visited == len(must_visit):
                memo[key] = 1
            else:
                memo[key] = 0
                
            return memo[key]

        memo[key] = 0
        neighbors = graph.get(node)
        if neighbors:
            for nxt in neighbors:
                memo[key] += dfs(nxt, visited)
        return memo[key]
    return dfs(start, 0)
   
# part 1 
print("part1")       
findPaths(fromTo, "you", "you")
print(len(paths))
print(788)
## part 2
paths = count_paths_with_both(fromTo, "svr", "out")
print(paths)
print(316291887968000)