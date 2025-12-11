f = open("Input10.txt")

def findBestPath(goalState : list, currentState : list, buttons: list):
    indexesToSet = goalState.index(True)
    

def applyButton(currentState : list, button):
    for change in button:
        currentState[change] != currentState[change]

class Machine :
    def __init__(self, line) :
        split = line.split(" ")
        self.GoalState = split[0]
        self.Buttons = split[1:-1 ]
        print(self.Buttons )
        self.ButtonsAsArray = list(map(lambda x: list(map(lambda y: int(y), x.strip("()").split(","))), self.Buttons)) 
        self.CurrentState = "["+ ("."*(len(self.GoalState)-2)) +"]"
        self.GoalStateAsArray = [c == "#" for c in self.GoalState]
        self.CurrentStateArray = [c == "#" for c in self.CurrentState]
        
    def print(self) :
        print("CurrentState:", self.CurrentState, "GoalState:", self.GoalState, "Buttons:", self.Buttons,)
        print("ButtonsAsArray:", self.ButtonsAsArray, "GoalAsArray:", self.GoalStateAsArray, "CurrentStatAsArray:", self.CurrentStateArray)
    
    def stepsToSolve(self) :
        while(self.GoalStateAsArray != self.CurrentStateArray):
            print()     

machines = map(lambda x: Machine(x), f.read().split('\n'))

for machine in machines:
    machine.print()
