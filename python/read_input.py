f = open("input.txt", "r")
lines = f.read().split('\n')

if not lines[-1]: lines.pop()
