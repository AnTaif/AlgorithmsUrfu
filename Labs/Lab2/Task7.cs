namespace Labs.Lab2;

/*
Задача 7. Точки и отрезки.
Ограничение по времени: 2 секунды
Ограничение по памяти: 16 мегабайт
    Дано N отрезков на числовой прямой и M точек на этой же прямой. Для каждой из
    данных точек определите, скольким отрезкам она принадлежит. Точка x считается
    принадлежащей отрезку с концами a и b, если выполняется двойное неравенство min(a, b)
    ≤ x ≤ max(a, b).
Формат входных данных:
    Первая строка входного файла INPUT.TXT содержит два целых числа N – число
    отрезков и M – число точек (1 ≤ N, M ≤ 10^5).
    В следующих N строках по два целых числа ai и bi – координаты концов соответствующего отрезка.
    В последней строке M целых чисел – координаты точек.
    Все числа во входном файле не превосходят по модулю 10^9.
Формат выходных данных:
    В выходной файл OUTPUT.TXT выведите M чисел – для каждой точки количество
    отрезков, в которых она содержится.
*/

public static class Task7
{
    private static string rootPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;

    public static void Run()
    {
        using var reader = new StreamReader(Path.Combine(rootPath, "Lab2/input7.txt"));
        using var writer = new StreamWriter(Path.Combine(rootPath, "Lab2/output7.txt"));

        var input = reader.ReadLine()!.Split();
        var n = int.Parse(input[0]);
        var m = int.Parse(input[1]);

        var segments = new (int A, int B)[n];
        var points = new int[m];
        
        for (var i = 0; i < n; i++)
        {
            var segmentCoords = reader.ReadLine()!.Split();
            var a = int.Parse(segmentCoords[0]);
            var b = int.Parse(segmentCoords[1]);
            segments[i] = new(Math.Min(a, b), Math.Max(a, b));
        }

        var pointsCoords = reader.ReadLine()!.Split();
        for (var i = 0; i < m; i++)
        {
            var point = int.Parse(pointsCoords[i]);
            points[i] = point;
        }

        var result = CountSegments(segments, points);

        writer.WriteLine(string.Join(" ", result));
    }

    public static int[] CountSegments((int A, int B)[] segments, int[] points)
    {
        QuickSorter.QuickSort(segments, 0, segments.Length - 1);

        var counts = new int[points.Length];
        for (var i = 0; i < points.Length; i++)
        {
            var point = points[i];
            var count = 0;

            foreach (var segment in segments)
            {
                if (InBounds(segment, point))
                    count++;
                else if (point < segment.A)
                    break;
            }

            counts[i] = count;
        }

        return counts;
    }

    private static bool InBounds((int A, int B) segment, int point) =>
        point >= segment.A && point <= segment.B;
    
    public static class QuickSorter
    {
        public static void QuickSort((int, int)[] arr, int low, int high)
        {
            if (low >= high) return;
        
            var pi = Partition(arr, low, high);
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }

        private static int Partition((int, int)[] arr, int low, int high)
        {
            var pivot = arr[high].Item1;
            var i = low - 1;

            for (var j = low; j <= high - 1; j++)
            {
                if (arr[j].Item1 >= pivot) continue;
            
                i++;
                Swap(arr, i, j);
            }
            Swap(arr, i + 1, high);
            return i + 1;
        }
    
        private static void Swap((int, int)[] arr, int i, int j)
        {
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }
}