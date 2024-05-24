namespace Labs.Lab7;

/*
Задача 2. Путешествие продавца
Ограничение по времени: 1 секунда
Ограничение по памяти: 64 мегабайта
    Как обычно, кому-то надо продать что-то во многих городах. Имеются города,
    представленные как M множеств (столбцов) по N городов (строк) в каждом.
    Продавец должен посетить ровно по одному городу из каждого множества, затратив
    на это как можно меньшую сумму денег. Он должен посетить сначала город из первого
    множества, затем из второго и так далее, строго по порядку. Он может выбирать начало
    своего путешествия. Число, которое находжится в i-й строке и j-м столбце означает
    стоимость перемещения из предыдущего места в этот город. Однако, имеется ограничение
    на перемещения: он может перемещаться из города в i-й строке только в города следующего
    столбца, находящиеся в одной из строк i -1, i, i+1, если такие строки существуют.
    Иногда, чтобы заставить посетить продавца какой-то город, ему доплачивают, то
    есть, стоимость перемещения может быть отрицательной.
    Требуется определить наименьшую стоимость маршрута и сам маршрут.
Формат входных данных
    N M
    C11 C12 … C1M
    C21 C22 … C2M
    … … … …
    CN1 CN2 … CNM
    3 ≤ N ≤ 150
    3 ≤ M ≤ 1000
    -1000 ≤ Cij ≤ 1000
Формат выходных данных
    Первая строка — список через пробел номеров строк (начиная с 1) из M посещенных городов.
    Вторая строка — общая стоимость поездки.
    Если имеется несколько маршрутов с одной стоимостью, требуется вывести
    маршрут, наименьший в лексикографическом порядке.
    Начинать и заканчивать маршрут можно в любой строке. 
*/

public static class Task2
{
    public static void Run()
    {
        var firstLine = Console.ReadLine()!.Split();
        var N = int.Parse(firstLine[0]);
        var M = int.Parse(firstLine[1]);
        
        var costs = new int[N, M];
        for (var i = 0; i < N; i++)
        {
            var line = Console.ReadLine()!.Split();
            for (var j = 0; j < M; j++)
            {
                costs[i, j] = int.Parse(line[j]);
            }
        }
        
        var result = Solve(costs, N, M);
        Console.WriteLine(string.Join(" ", result.Cities));
        Console.WriteLine(result.Cost);
    }

    public static (int[] Cities, int Cost) Solve(int[,] costs, int N, int M)
    {
        var dp = new int[N, M];
        var path = new int[N, M];
        
        for (var i = 0; i < N; i++)
        {
            dp[i, 0] = costs[i, 0];
            path[i, 0] = -1; // старт
        }
        
        for (var j = 1; j < M; j++)
        {
            for (var i = 0; i < N; i++)
            {
                dp[i, j] = int.MaxValue;
                for (var k = -1; k <= 1; k++)
                {
                    var prevRow = i + k;
                    if (prevRow < 0 || prevRow >= N) 
                        continue;
                    
                    var newCost = dp[prevRow, j - 1] + costs[i, j];
                    if (newCost >= dp[i, j] && (newCost != dp[i, j] || prevRow >= path[i, j])) 
                        continue;
                    
                    dp[i, j] = newCost;
                    path[i, j] = prevRow;
                }
            }
        }

        var minCost = int.MaxValue;
        var lastCity = -1;
        for (var i = 0; i < N; i++)
        {
            if (dp[i, M - 1] < minCost)
            {
                minCost = dp[i, M - 1];
                lastCity = i;
            }
        }
        
        var cities = new int[M];
        for (int j = M - 1, i = lastCity; j >= 0; j--)
        {
            cities[j] = i + 1;
            i = path[i, j];
        }

        return (cities, minCost);
    }
}