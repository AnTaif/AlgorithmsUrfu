namespace Labs.Lab7;

/*
Задача 6. Научная конференция
Ограничение по времени: 1 секунда
Ограничение по памяти: 64 мегабайта
    Работа научной конференции обычно разделена на несколько одновременно
    проходящих секций. Например, может быть секция параллельных вычислений, секция
    визуализации, секция сжатия данных и так далее.
    Очевидно, одновременная работа нескольких секций необходима, чтобы
    уменьшить время научной программы конференции и иметь больше времени на банкет,
    чаепитие и неофициальные обсуждения. Однако интересные доклады могут проходить
    одновременно в разных секциях.
    Участник записал расписание всех докладов, интересных ему. Он просит вас
    определить максимальное количество докладов, которые он сможет посетить.
Формат входных данных
    Первая строка содержит количество 1 ≤ N ≤ 100 000 интересных докладов. Каждая
    из следующих N строк содержит два целых числа Ts и Te, разделённых пробелом (1 ≤ Ts <
    Te ≤ 30 000). Эти числа — время начала и конца соответствующего доклада. Время задано
    в минутах от начала конференции.
Формат выходных данных
    Выведите максимальное количество докладов, которые участник может посетить.
    Участник не может посетить два доклада, идущих одновременно, и любые два доклада,
    которые он посещает, должны быть разделены хотя бы одной минутой. Например, если
    доклад кончается в 15, следующий доклад, который может быть посещён, должен
    начинаться в 16 или позже.
*/

public static class Task6
{
    public static void Run()
    {
        var N = int.Parse(Console.ReadLine()!);
        var reports = new (int Start, int End)[N];
        for (var i = 0; i < N; i++)
        {
            var line = Console.ReadLine()!.Split();

            reports[i] = (int.Parse(line[0]), int.Parse(line[1]));
        }

        var result = Solve(reports);
        
        Console.WriteLine(result);
    }

    public static int Solve((int Start, int End)[] reports)
    {
        // Сортируем доклады по времени окончания
        Array.Sort(reports, (a, b) => a.End.CompareTo(b.End));

        // dp[i] - максимальное количество докладов,
        // которые можно посетить, закончив на i-ом докладе
        var dp = new int[reports.Length];

        for (var i = 1; i <= reports.Length - 1; i++)
        {
            // Выбираем наибольшее из двух вариантов:
            // 1. Продолжить последовательность, посетив i-ый доклад
            // 2. Начать новую последовательность, начиная с i-го доклада
            dp[i] = Math.Max(dp[i - 1], 1);

            // Проверяем, совместим ли i-ый доклад с предыдущими
            // в dp[j]
            for (var j = i - 1; j >= 0; j--)
            {
                if (reports[j].End <= reports[i].Start)
                {
                    dp[i] = Math.Max(dp[i], dp[j] + 1);
                }
            }
        }

        // Возвращаем максимальное количество докладов
        return dp[reports.Length - 1];
    }
}