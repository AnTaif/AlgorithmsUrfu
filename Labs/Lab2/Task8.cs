namespace Labs.Lab2;

/*
Задача 8. Охрана.
Ограничение по времени: 1 секунда
Ограничение по памяти: 16 мегабайт
    На секретной военной базе работает N охранников. Сутки поделены на 10000 равных
    промежутков времени, и известно когда каждый из охранников приходит на дежурство и
    уходит с него. Например, если охранник приходит в 5, а уходит в 8, то значит, что он был в
    6, 7 и 8-ой промежуток. В связи с уменьшением финансирования часть охранников решено
    было сократить. Укажите: верно ли то, что для данного набора охранников, объект
    охраняется в любой момент времени хотя бы одним охранником и удаление любого из них
    приводит к появлению промежутка времени, когда объект не охраняется.
Формат входных данных:
    В первой строке входного файла INPUT.TXT записано натуральное число K (1 ≤ K
    ≤ 30) – количество тестов в файле. Каждый тест начинается с числа N (1 ≤ N ≤ 10000), за
    которым следует N пар неотрицательных целых чисел A и B - время прихода на дежурство
    и ухода (0 ≤ A < B ≤ 10000) соответствующего охранника. Все числа во входном файле
    разделены пробелами и/или переводами строки.
Формат выходных данных:
    В выходной файл OUTPUT.TXT выведите K строк, где в M-ой строке находится
    слово Accepted, если M-ый набор охранников удовлетворяет описанным выше условиям. В
    противном случае выведите Wrong Answer
*/

public static class Task8
{
    private const string WrongAnswer = "Wrong Answer";
    private const string TrueAnswer = "Accepted";
    
    private static string rootPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;

    public static void Run()
    {
        using var reader = new StreamReader(Path.Combine(rootPath, "Lab2/input8.txt"));
        using var writer = new StreamWriter(Path.Combine(rootPath, "Lab2/output8.txt"));

        var k = int.Parse(reader.ReadLine()!);
        var arr = new List<Tuple<int, int>>[k];

        for (var i = 0; i < k; i++)
        {
            var inputTest = reader.ReadLine()!.Split();
            var n = int.Parse(inputTest[0]);

            arr[i] = new List<Tuple<int, int>>(n);
            
            for (var j = 0; j < n; j++)
            {
                var index = j * 2 + 1;
                var inputTuple = new Tuple<int, int>(int.Parse(inputTest[index]), int.Parse(inputTest[index+1]));

                arr[i].Add(inputTuple);
            }
        }

        for (var i = 0; i < k; i++)
        {
            var answer = Solve(arr[i].ToArray()) ? TrueAnswer : WrongAnswer;
            writer.WriteLine(answer);
        }
    }

    public static bool Solve(Tuple<int, int>[] periods)
    {
        var prevEnd = 0;
        
        QuickSorter.QuickSort(periods, 0, periods.Length - 1);

        for (var i = 0; i < periods.Length; i++)
        {
            var period = periods[i];
            
            if (period.Item1 > prevEnd)
                return false;

            if (i >= 2 && periods[i - 2].Item2 >= period.Item1)
                return false;

            prevEnd = Math.Max(period.Item2, prevEnd);
        }

        return true;
    }
}