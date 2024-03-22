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
        var n = input[0];
        var m = input[1];
        
        
    }

    public static int[] Solve()
    {
        
    } 
}