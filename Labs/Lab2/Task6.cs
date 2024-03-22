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
    private static string rootPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
    
    public static void Run()
    {
        using var reader = new StreamReader(Path.Combine(rootPath, "Lab2/input6.txt"));
        using var writer = new StreamWriter(Path.Combine(rootPath, "Lab2/output6.txt"));
        
        var n = int.Parse(reader.ReadLine()!);

        var visits = new List<(int Arrival, int Departure)>();

        for (var i = 0; i < n; i++)
        {
            var input = reader.ReadLine()!.Split();
            var arrivalTime = input[0].Split(':');
            var departureTime = input[1].Split(':');

            var arrival = int.Parse(arrivalTime[0]) * 60 + int.Parse(arrivalTime[1]);
            var departure = int.Parse(departureTime[0]) * 60 + int.Parse(departureTime[1]);

            visits.Add((arrival, departure));
        }

        visits.Sort();

        var maxVisitors = 0;
        var currentVisitors = 0;
        
        var baseVisit = visits[0];

        foreach (var visit in visits)
        {
            if (visit.Departure >= visit.Arrival)
                while (visits.Count > 0 && baseVisit.Arrival <= visit.Departure)
                {
                    currentVisitors++;
                    baseVisit = visits[0];
                }

            if (currentVisitors > maxVisitors)
                maxVisitors = currentVisitors;
        }
        
        writer.WriteLine(maxVisitors);
    } 
}