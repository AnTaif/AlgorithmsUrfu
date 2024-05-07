using System.Globalization;

namespace Labs.Lab6;

/*
Задача 10. Коррозия металла
Ограничение по времени: 1 секунда
Ограничение по памяти: 16 мегабайт
    Для хранения двух агрессивных жидкостей A и B используется емкость с
    многослойной перегородкой, которая изготавливается из имеющихся N листов. Для
    каждого листа i (i = 1, …, N) известно время его растворения жидкостью A — ai и жидкостью B — bi. 
    Растворение перегородки каждой из жидкостей происходит
    последовательно лист за листом, с постоянной скоростью по толщине листа.
    Требуется написать программу проектирования такой перегородки, время
    растворения которой было бы максимальным.
Формат входных данных:
    В первой строке входного файла INPUT.TXT записано число N (1 ≤ N ≤ 256). 
    В каждой из последующих N строк содержатся два положительных вещественных числа ai и bi, разделенные пробелом
    (числа не превышают 10^6 и состоят не более чем из 11 значащих цифр).
Формат выходных данных:
    В первую строку выходного файла OUTPUT.TXT записать время растворения
    перегородки с точностью, не меньшей 10^-3. В следующую строку файла записать номера листов в порядке их расположения
    от жидкости A к жидкости B, разделяя числа пробелами.
*/

public static class Task10
{
    public static void Run()
    {
        // var N = int.Parse(Console.ReadLine()!);
        // var sheets = new Sheet[N];
        // for (var i = 0; i < N; i++)
        // {
        //     var sheetLine = Console.ReadLine()!.Split().Select(double.Parse).ToArray();
        //     sheets[i] = new Sheet(sheetLine[0], sheetLine[1], i+1);
        // }

        var sheets = new Sheet[]
        {
            new(1, 2, 1),
            new(1, 2, 2),
            new(0.5, 1.5, 3),
            new(7, 3.5, 4)
        };

        var (time, numbers) = Solve(sheets);
        
        Console.WriteLine(time.ToString("F3", CultureInfo.InvariantCulture));
        Console.WriteLine(string.Join(" ", numbers));
    }

    public static (double Time, int[] Numbers) Solve(Sheet[] sheets)
    {
        // Сортируем по уменьшению времени разъедания первой жидкостью
        Array.Sort(sheets, (sheet1, sheet2) => (sheet2.A / sheet2.B).CompareTo(sheet1.A / sheet1.B));
        
        var totalTime = sheets.Sum(sheet => sheet.A);
        double maxTime = 0;

        for (var i = 0; i < sheets.Length; i++)
        {
            var currentSheet = sheets[i];
            totalTime -= currentSheet.A;
            var currentTime = totalTime + currentSheet.B;

            if (currentTime > maxTime)
                maxTime = currentTime;
        }

        return (maxTime, sheets.Select(sheet => sheet.Number).ToArray());
    }
}

public record Sheet(double A, double B, int Number);