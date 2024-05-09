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
        string[] routes = {
            "3 4 6 2 8 6",
            "6 1 8 2 7 4",
            "5 9 3 9 9 5",
            "8 4 1 3 9 6",
            "3 7 2 8 6 4",
            // "1 7 4 3",
            // "5 1 6 7",
            // "4 1 9 2",
            // "7 3 7 5",
            // "8 2 4 1",
        };
        
        var result = Solve(routes);
        
        Console.WriteLine(string.Join(" ", result.Cities.Select(c => c + 1)));
        Console.WriteLine(result.Cost);
    }

    public static (int[] Cities, int Cost) Solve(string[] routes)
    {
        int n = routes.Length;
        int m = routes[0].Split().Length;

        // Массив для хранения минимальной стоимости посещения каждого города из каждого множества
        int[,] dp = new int[n, m];

        // Заполняем массив dp снизу вверх, начиная с последнего множества
        for (int i = n - 1; i >= 0; i--)
        {
            string[] costs = routes[i].Split();
            for (int j = 0; j < m; j++)
            {
                int currentCost = int.Parse(costs[j]);

                // Если это последнее множество, то минимальная стоимость равна просто стоимости города
                if (i == n - 1)
                {
                    dp[i, j] = currentCost;
                }
                else
                {
                    int minCost = int.MaxValue;
                    // Перебираем все возможные следующие города из следующего множества
                    for (int k = -1; k <= 1; k++)
                    {
                        int nextRow = i + 1;
                        int nextColumn = j + k;

                        // Проверяем, что следующий город существует в следующем множестве
                        if (nextColumn >= 0 && nextColumn < m && nextRow < n)
                        {
                            // Обновляем минимальную стоимость
                            minCost = Math.Min(minCost, dp[nextRow, nextColumn]);
                        }
                    }
                    // Минимальная стоимость для текущего города
                    dp[i, j] = currentCost + minCost;
                }
            }
        }

        // Находим минимальную стоимость маршрута и сам маршрут
        int minTotalCost = int.MaxValue;
        int[] cities = new int[n];
        int minStartCity = 0;
        for (int j = 0; j < m; j++)
        {
            if (dp[0, j] < minTotalCost)
            {
                minTotalCost = dp[0, j];
                minStartCity = j;
            }
        }

        cities[0] = minStartCity;
        int currentCity = minStartCity;
        for (int i = 1; i < n; i++)
        {
            int minNextCity = currentCity;
            int minNextCost = int.MaxValue;
            for (int k = -1; k <= 1; k++)
            {
                int nextColumn = currentCity + k;
                if (nextColumn >= 0 && nextColumn < m)
                {
                    if (dp[i, nextColumn] < minNextCost)
                    {
                        minNextCost = dp[i, nextColumn];
                        minNextCity = nextColumn;
                    }
                }
            }
            cities[i] = minNextCity;
            currentCity = minNextCity;
        }

        return (cities, minTotalCost);
    }
}