namespace Labs.Lab7;

/*
Задача 9. Миллиардная Функция Васи
Ограничение по времени: 1 секунда
Ограничение по памяти: 64 мегабайта
    Вася — начинающий математик — решил сделать вклад в развитие этой науки и
    прославиться на весь мир. Но как это сделать, когда самые интересные факты, типа теоремы
    Пифагора, давно уже доказаны? Правильно! Придумать что-то свое, оригинальное. Вот
    юный математик и придумал Теорию Функций Васи, посвященную изучению поведения
    этих самых функций. Функции Васи (ФВ) устроены довольно просто: значением N-й ФВ в
    точке S будет количество чисел от 1 до N, имеющих сумму цифр S. Вам, как крутым
    программистам, Вася поручил найти значения миллиардной ФВ (то есть ФВ с N = 10^9), так
    как сам он с такой задачей не справится. А Вам слабо?
Формат входных данных
    Целое число S (1 ≤ S ≤ 81).
Формат выходных данных
    Значение миллиардной Функции Васи в точке S.
*/

/*
1  -> 10
2  -> 45
3  -> 165
10 -> 43749
40 -> 45433800
80 -> 9
81 -> 1
*/

public static class Task9
{
    private const int N = 9;
    private const int MaxS = 81;
    
    public static void Run()
    {
        var s = int.Parse(Console.ReadLine()!);
        Console.WriteLine(Solve(s));
    }
    
    public static int Solve(int s)
    {
        var dp = new int[MaxS+1, N+1];
        
        // Инициализация базовых значений
        for (var i = 1; i <= N; i++)
            dp[i, 1] = 1;
        
        // Заполнение массива
        for (var i = 1; i <= MaxS; i++)
        {
            for (var j = 2; j <= N; j++)
            {
                for (var k = 0; k <= N; k++)
                {
                    if (i >= k)
                        dp[i, j] += dp[i - k, j - 1];
                }
            }
        }
        
        // Добавляем 0 и максимальное значение 10^N
        dp[0, N] = 1;
        dp[1, N] += 1;

        var result = 0;
        for (var i = 1; i <= N; i++)
        {
            result += dp[s, i];
        }

        return result;
    }
}