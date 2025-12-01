## Day 1
f = open("Input1.txt")
lines = f.read().splitlines()
currentPlace = 50
maxPlace = 99
password = 0

# for data in lines:
#     chars = list(data)
#     lOrR = chars[0];
#     steps = int(''.join(chars[1:]))
#     print("before","current", currentPlace, "steps", steps, "lOrR", lOrR)
#     currentPlace += steps if lOrR == "R" else -steps
#     currentPlace %= maxPlace + 1
            
#     if currentPlace == 0:
#         password += 1
    
#     print(currentPlace, password)
    
# print("Password: " + str(password))
# print("correct would be: 1076")

for data in lines:
    chars = list(data)
    lOrR = chars[0];
    steps = int(''.join(chars[1:]))
    
    i = 0;

    while i < steps:
        if lOrR == "R":
            currentPlace += 1
        else:
            currentPlace -= 1            
        if currentPlace % 100 == 0:
            password += 1
        i += 1
    
    print(currentPlace, password)
print("Password: " + str(password))