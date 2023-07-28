def evolve_soldiers(soldiers, m):
    n = len(soldiers)
    for _ in range(m):
        new_soldiers = [0] * n
        for i in range(n):
            if soldiers[i] == 1:
                new_soldiers[i] = 1
            else:
                live_neighbors = (
                    soldiers[i-1] if i > 0 else 0) + (soldiers[i+1] if i < n-1 else 0)
                if live_neighbors == 1:
                    new_soldiers[i] = 1
        soldiers = new_soldiers
    return ''.join(str(s) for s in soldiers)


t = int(input())
for _ in range(t):
    n, m = map(int, input().split())
    soldiers = [int(s) for s in input().strip()]
    print(evolve_soldiers(soldiers, m))
