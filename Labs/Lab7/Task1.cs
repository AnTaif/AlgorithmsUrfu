namespace Labs.Lab7;

/*
Задача 1. Игра в фишки
Ограничение по времени: 1 секунда
Ограничение по памяти: 16 мегабайта
    На столе лежит куча из 1 ≤ N ≤ 10^6 фишек. Игроки First и Second ходят строго по
    очереди, первый ход за игроком First. Каждый ход игрок может взять из кучи любое
    количество фишек, не превосходящее целой части квадратного корня из оставшегося на
    столе количества фишек. Например, при 28 фишках на столе он может взять от одной до
    пяти фишек. Игра заканчивается, когда на столе не остается ни одной фишки и победителем
    объявляется тот, кто совершил последний ход.
    Требуется вывести имя игрока, который побеждает при обоюдно лучшей игре.
Формат входных данных
    N
Формат выходных данных
    First
    или
    Second
*/

public static class Task1
{
    public static void Run()
    {
        int N = int.Parse(Console.ReadLine()!);
        Console.WriteLine(Solve(N));
    }

    public static string Solve(int N)
    {
        bool[] canWin = new bool[N + 1];

        for (int i = N; i > 0; i--)
        {
            int sqrt = (int)Math.Sqrt(i);
            for (int k = sqrt; k >0; k--)
            {
                if (!canWin[i - k*k])
                {
                    canWin[i] = true;
                    break;
                }
            }
        }

        return canWin[N] ? "First" : "Second";
    }
}