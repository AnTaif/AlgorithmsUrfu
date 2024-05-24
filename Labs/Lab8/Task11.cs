using System.Globalization;

namespace Labs.Lab8;

/*
Задача 11. На метро или пешком?     https://acm.timus.ru/problem.aspx?space=1&num=1205&locale=ru
Ограничение по времени: 1 секунда
Ограничение по памяти: 64 мегабайта
    Представьте себя в большом городе. Вы хотите попасть из точки A в точку B. Для
    этого вы можете идти пешком или использовать метро. Перемещение с помощью метро
    быстрее, но входить в метро и выходить из него можно только на станциях. Чтобы
    сэкономить время, вы решили написать программу нахождения самого быстрого пути.
Формат входных данных
    Первая строка содержит два вещественных числа: скорость ходьбы и скорость
    перемещения с помощью метро. Вторая скорость всегда больше первой.
    Затем следует описание метро. В первой строке описания находится целое число N
    — количество станций метро. Можете считать, что N не превосходит 200. Следующие N
    строк содержат по два вещественных числа каждая (i-я строка содержит координаты i-й
    станции). Затем следует описание соединений между станциями. Каждое соединение
    определяется парой целых чисел — номерами соединённых станций. Список соединений
    завершает пара нулей. Будем считать, что станции соединяются по прямой, так что время
    перемещения между станциями равно расстоянию между станциями, делённому на
    скорость перемещения с помощью метро.
    Заметим, что вход и выход из метро, а также смена поезда возможны только на
    станциях и не требуют дополнительного времени.
    Последними даны координаты точек A и B по паре координат в строке.
Формат выходных данных
    Первая строка должна содержать минимальное время, необходимое, чтобы попасть
    из точки A в точку B. Время должно быть выведено с точностью 10−6. 
    Вторая строка описывает использование метро при передвижении. Она начинается количеством станций,
    которые нужно посетить, затем следует список номеров станций в порядке, в котором их
    нужно посетить.
*/

public static class Task11
{
    public static void Run()
    {
        var statInput = Console.ReadLine()!.Split();
        var walkSpeed = double.Parse(statInput[0], CultureInfo.InvariantCulture);
        var metroSpeed = double.Parse(statInput[1], CultureInfo.InvariantCulture);
        
        var n = int.Parse(Console.ReadLine()!);
        
        var stations = new (double X, double Y)[n];
        for (var i = 0; i < n; i++)
        {
            var stationInput = Console.ReadLine()!.Split();
            stations[i] = (
                X: double.Parse(stationInput[0], CultureInfo.InvariantCulture), 
                Y: double.Parse(stationInput[1], CultureInfo.InvariantCulture));
        }
        
        var edges = new List<(int From, int To)>();
        while (true)
        {
            var edgeInput = Console.ReadLine()!.Split();
            var from = int.Parse(edgeInput[0]) - 1;
            var to = int.Parse(edgeInput[1]) - 1;
            if (from == -1 && to == -1)
                break;
            edges.Add((from, to));
        }

        var pointAInput = Console.ReadLine()!.Split();
        var pointA = (
            double.Parse(pointAInput[0], CultureInfo.InvariantCulture), 
            double.Parse(pointAInput[1], CultureInfo.InvariantCulture));

        var pointBInput = Console.ReadLine()!.Split();
        var pointB = (
            double.Parse(pointBInput[0], CultureInfo.InvariantCulture), 
            double.Parse(pointBInput[1], CultureInfo.InvariantCulture));


        var (time, usingStations) = Solve(n, walkSpeed, metroSpeed, stations, edges, pointA, pointB);

        Console.WriteLine(time.ToString("F7", CultureInfo.InvariantCulture));
        Console.WriteLine(usingStations.Length);
        if (usingStations.Length > 0)
        {
            Console.WriteLine(string.Join(" ", usingStations));
        }
    }

    public static (double Time, int[] Using) Solve(
        int n, double walkSpeed, double metroSpeed,
        (double X, double Y)[] stations, 
        List<(int From, int To)> edges, 
        (double, double) pointA, 
        (double, double) pointB)
    {
        var totalVertices = n + 2;

        var adj = new List<(int To, double Weight)>[totalVertices];
        for (var i = 0; i < totalVertices; i++)
            adj[i] = new List<(int To, double Weight)>();

        AddEdges(adj, n, walkSpeed, metroSpeed, stations, edges, pointA, pointB);

        // Алгорит Дейкстры от точки А до точки Б
        var dist = new double[totalVertices];
        var prev = new int[totalVertices];
        var visited = new bool[totalVertices];

        for (var i = 0; i < totalVertices; i++)
        {
            dist[i] = double.MaxValue;
            prev[i] = -1;
        }
        dist[n] = 0;

        var pq = new SortedSet<(double Dist, int Vertex)> { (0, n) };

        while (pq.Count > 0)
        {
            var (currentDist, u) = pq.Min;
            pq.Remove(pq.Min);

            if (visited[u])
                continue;

            visited[u] = true;

            foreach (var (v, weight) in adj[u])
            {
                var alt = currentDist + weight;
                
                if (!(alt < dist[v])) continue;
                
                pq.Remove((dist[v], v));
                dist[v] = alt;
                prev[v] = u;
                pq.Add((alt, v));
            }
        }

        // Восстанавливаю путь
        var path = new List<int>();
        for (var at = n + 1; at != -1; at = prev[at])
        {
            if (at < n)
                path.Add(at + 1);
        }
        path.Reverse();

        return (dist[n + 1], path.ToArray());

        // Добавляю соединения в граф
    }

    private static void AddEdges(List<(int To, double Weight)>[] adj, 
        int n, double walkSpeed, double metroSpeed,
        (double X, double Y)[] stations, 
        List<(int From, int To)> edges, 
        (double, double) pointA, 
        (double, double) pointB)
    {
        // Добавляю соединения между станициями
        foreach (var (from, to) in edges)
        {
            var distance = GetDistance(stations[from], stations[to]);
            var time = distance / metroSpeed;
            AddEdge(adj, from, to, time);
        }

        // Добавляю соединения от точки А до всех станция и до точки Б
        for (var i = 0; i < n; i++)
        {
            var distanceToA = GetDistance(pointA, stations[i]);
            var timeToA = distanceToA / walkSpeed;
            AddEdge(adj, n, i, timeToA);

            var distanceToB = GetDistance(pointB, stations[i]);
            var timeToB = distanceToB / walkSpeed;
            AddEdge(adj, n + 1, i, timeToB);
        }

        // Добавляю прямое соединения от точки А до точки Б
        var directDistance = GetDistance(pointA, pointB);
        var directTime = directDistance / walkSpeed;
        AddEdge(adj, n, n + 1, directTime);
    }

    private static void AddEdge(List<(int To, double Weight)>[] adj, int from, int to, double weight)
    {
        adj[from].Add((to, weight));
        adj[to].Add((from, weight));
    }

    private static double GetDistance((double X, double Y) point1, (double X, double Y) point2)
    {
        var dx = point1.X - point2.X;
        var dy = point1.Y - point2.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}

/*
Решение с помощью алгоритма Дейкстры
 
Определяется общее количество вершин в графе, включающее количество станций метро и две дополнительные точки (A и B).

Создается список смежности для хранения графа, где для каждой вершины указывается список соседних вершин и вес ребра между ними.

Добавляются ребра между станциями метро на основе заданных соединений. 
Вес ребра определяется как время, необходимое для прохождения расстояния между станциями с учетом скорости метро.

Добавляются ребра от точки A ко всем станциям метро и к точке B, а также от точки B ко всем станциям метро. 
Вес этих ребер определяется как время, необходимое для прохождения расстояния между точками с учетом скорости ходьбы.

Добавляется прямое ребро между точками A и B с весом, равным времени прохождения расстояния между ними с учетом скорости ходьбы.

Выполняется алгоритм Дейкстры для поиска кратчайшего пути от точки A до точки B. 
Для этого инициализируются массивы расстояний и предыдущих вершин, а также множество для отслеживания непосещенных вершин.

В процессе работы алгоритма для каждой вершины обновляются минимальные расстояния до соседних вершин
и сохраняется информация о предыдущих вершинах.

После завершения алгоритма восстанавливается путь от точки B до точки A, используя массив предыдущих вершин.

Возвращается время, необходимое для прохождения кратчайшего пути, и сам путь в виде массива вершин.

Вспомогательная функция AddEdge добавляет ребро в граф, 
а функция GetDistance вычисляет евклидово расстояние между двумя точками.
*/