namespace Labs.Lab2;

/*
Задача 6. Музей.
Ограничение по времени: 1 секунда
Ограничение по памяти: 16 мегабайт
    В музее регистрируется в течение суток время прихода и ухода каждого посетителя.
    Таким образом, за день получены N пар значений, где первое значение в паре показывает
    время прихода посетителя и второе значение - время его ухода. Требуется найти
    максимальное число посетителей, которые находились в музее одновременно.
Формат входных данных:
    В первой строке входного файла INPUT.TXT записано натуральное число N (N < 10^5) - количество
    зафиксированных посетителей в музее в течении суток. Далее, идут N
    строк с информацией о времени визитов посетителей: в каждой строке располагается
    отрезок времени посещения в формате «ЧЧ:ММ ЧЧ:ММ» (00:00 ≤ ЧЧ:ММ ≤ 23:59).
Формат выходных данных:
    В единственную строку выходного файла OUTPUT.TXT нужно вывести одно целое
    число — максимальное количество посетителей, одновременно находящихся в музее.
*/

public static class Task6
{
    private static readonly string RootPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
    
    private const string Start = "start";
    private const string End = "end";
    
    public static void Run()
    {
        using var reader = new StreamReader(Path.Combine(RootPath, "Lab2/input6.txt"));
        using var writer = new StreamWriter(Path.Combine(RootPath, "Lab2/output6.txt"));
        
        var n = int.Parse(reader.ReadLine()!);

        var visits = new List<(int Time, string Type)>();

        for (var i = 0; i < n; i++)
        {
            var input = reader.ReadLine()!.Split();
            var arrivalTime = input[0].Split(':');
            var departureTime = input[1].Split(':');

            visits.Add((int.Parse(arrivalTime[0]) * 60 + int.Parse(arrivalTime[1]), Start));
            visits.Add((int.Parse(departureTime[0]) * 60 + int.Parse(departureTime[1]), End));
        }

        QuickSorter.QuickSort(visits, 0, visits.Count - 1);

        var maxVisitors = 0;
        var currentVisitors = 0;

        foreach (var visit in visits)
        {
            if (visit.Type == Start)
                currentVisitors++;
            else
                currentVisitors--;

            maxVisitors = Math.Max(currentVisitors, maxVisitors);
        }
        
        writer.WriteLine(maxVisitors);
    }
    
    public static class QuickSorter
    {
        public static void QuickSort(List<(int, string)> list, int low, int high)
        {
            if (low >= high) return;
        
            var pi = Partition(list, low, high);
            QuickSort(list, low, pi - 1);
            QuickSort(list, pi + 1, high);
        }

        private static int Partition(List<(int, string)> list, int low, int high)
        {
            var pivot = list[high].Item1;
            var i = low - 1;

            for (var j = low; j <= high - 1; j++)
            {
                if (list[j].Item1 >= pivot) continue;
            
                i++;
                Swap(list, i, j);
            }
            Swap(list, i + 1, high);
            return i + 1;
        }
    
        private static void Swap(List<(int, string)> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}