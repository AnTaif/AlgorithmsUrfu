namespace Labs.Lab8;

/*
Задача 5. Кратчайшие пути
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайта
    Взвешенный граф с N вершинами задан своими M ребрами Ei, возможно
    отрицательного веса. Требуется найти все кратчайшие пути от вершины S до остальных
    вершин. Если граф содержит отрицательные циклы, вывести IMPOSSIBLE. Если от
    вершины S до какой-либо из вершин нет маршрута, то в качестве длины маршрута вывести
    слово UNREACHABLE.
    Вершины графа нумеруются, начиная с нуля.
    3 ≤ N ≤ 800
    1 ≤ M ≤ 30000
    -1000 ≤ Wi ≤ 1000
Формат входных данных:
    N M S
    S1 E1 W1
    S2 E2 W2
    ...
Формат выходных данных:
    IMPOSSIBLE
    или
    D1 D2 D3 … DN
Где Di может быть UNREACHABLE
*/

public static class Task5
{
    private const int INF = int.MaxValue;
    
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var N = int.Parse(input[0]);
        var M = int.Parse(input[1]);
        var S = int.Parse(input[2]);
        
        var edges = new Edge[M];
        for (var i = 0; i < M; i++)
        {
            var edgeInput = Console.ReadLine()!.Split();
            var u = int.Parse(edgeInput[0]);
            var v = int.Parse(edgeInput[1]);
            var w = int.Parse(edgeInput[2]);
            edges[i] = new Edge(u, v, w);
        }
        
        var result = Solve(edges, N, S);
        
        foreach (var line in result)
            Console.WriteLine(line);
    }

    public static string[] Solve(Edge[] edges, int N, int S)
    {
        var distances = new int[N];
        for (var i = 0; i < N; i++)
            distances[i] = INF;
        
        distances[S] = 0;

        // Алгоритм Беллмана-Форда
        for (var i = 0; i < N - 1; i++)
        {
            var anyChange = false;
            foreach (var edge in edges)
            {
                if (distances[edge.U] == INF || distances[edge.V] <= distances[edge.U] + edge.W) 
                    continue;
                
                distances[edge.V] = distances[edge.U] + edge.W;
                anyChange = true;
            }
            if (!anyChange) break;
        }

        // Проверка на негативные веса
        if (edges.Any(edge => distances[edge.U] != INF && distances[edge.U] + edge.W < distances[edge.V]))
            return new[] { "IMPOSSIBLE" };
        
        var result = new string[N];
        for (var i = 0; i < N; i++)
            if (distances[i] == INF)
                result[i] = "UNREACHABLE";
            else
                result[i] = distances[i].ToString();
        
        return result;
    }

    public class Edge(int u, int v, int w)
    {
        public int U { get; } = u;
        public int V { get; } = v;
        public int W { get; } = w;
    }
}

/*
Алгоритм Беллмана-Форда для нахождения кратчайших путей 
в графе с возможностью наличия ребер с отрицательными весами.

Создается массив distances, в котором будет храниться кратчайшее расстояние от начальной вершины S до каждой другой вершины.
Все расстояния инициализируются значением INF (предположительно, константа, обозначающая бесконечность).
Расстояние до начальной вершины S устанавливается в 0.

Внешний цикл выполняется N-1 раз (по количеству вершин минус один).
Внутри цикла перебираются все ребра.
Для каждого ребра проверяется, можно ли улучшить текущее кратчайшее расстояние до конечной вершины V через начальную вершину U.
Если улучшается, обновляется значение distances[edge.V] и устанавливается флаг anyChange в true.
Если на очередной итерации внешний цикл не находит изменений (anyChange остается false), он прерывается досрочно.

После выполнения основного алгоритма выполняется проверка на наличие отрицательных циклов.
Если можно улучшить какое-либо расстояние после N-1 итераций, значит, существует отрицательный цикл, и функция возвращает ["IMPOSSIBLE"].

Создается массив строк result для хранения итоговых расстояний.
Если расстояние до вершины равно INF, это значит, что вершина недостижима, и в result записывается "UNREACHABLE".
Иначе в result записывается строковое представление расстояния.
*/