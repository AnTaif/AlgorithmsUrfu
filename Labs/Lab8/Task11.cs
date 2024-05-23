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
        // Total number of vertices in the graph: n stations + 2 points (A and B)
        var totalVertices = n + 2;

        // Adjacency list to store the graph
        var adj = new List<(int To, double Weight)>[totalVertices];
        for (var i = 0; i < totalVertices; i++)
            adj[i] = new List<(int To, double Weight)>();

        // Add edges between stations based on metro connections
        foreach (var (from, to) in edges)
        {
            var distance = GetDistance(stations[from], stations[to]);
            var time = distance / metroSpeed;
            AddEdge(from, to, time);
        }

        // Add edges from point A to all stations and to point B
        for (var i = 0; i < n; i++)
        {
            var distanceToA = GetDistance(pointA, stations[i]);
            var timeToA = distanceToA / walkSpeed;
            AddEdge(n, i, timeToA);

            var distanceToB = GetDistance(pointB, stations[i]);
            var timeToB = distanceToB / walkSpeed;
            AddEdge(n + 1, i, timeToB);
        }

        // Add direct edge from point A to point B
        var directDistance = GetDistance(pointA, pointB);
        var directTime = directDistance / walkSpeed;
        AddEdge(n, n + 1, directTime);

        // Perform Dijkstra's algorithm from point A (index n) to point B (index n + 1)
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

        // Reconstruct the path
        var path = new List<int>();
        for (var at = n + 1; at != -1; at = prev[at])
        {
            if (at < n)
                path.Add(at + 1);
        }
        path.Reverse();

        return (dist[n + 1], path.ToArray());

        // Function to add an edge to the graph
        void AddEdge(int from, int to, double weight)
        {
            adj[from].Add((to, weight));
            adj[to].Add((from, weight));
        }
    }

    private static double GetDistance((double X, double Y) point1, (double X, double Y) point2)
    {
        var dx = point1.X - point2.X;
        var dy = point1.Y - point2.Y;
        return Math.Sqrt(dx * dx + dy * dy);
    }
}