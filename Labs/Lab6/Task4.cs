namespace Labs.Lab6;

/*
Задача 4. Ровно M простых.
Ограничение по времени: 2 секунды
Ограничение по памяти: 24 мегабайта
    Требуется найти такое наименьшее натуральное число 2 ≤ K ≤ 2*10^7, что, начиная с этого числа, среди
    N натуральных чисел имеется ровно M простых.
    Если такого числа не существует или оно больше 2*10^7, вывести -1.
Формат входных данных:
    M N
Формат выходных данных:
    K или -1
*/

public static class Task4
{
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var M = int.Parse(input[0]);
        var N = int.Parse(input[1]);

        var result = Solve(M, N);
        
        Console.WriteLine(result);
    }

    public static int Solve(int M, int N)
    {
        var left = 2;
        var right = left + N - 1;

        while (right <= 2 * Math.Pow(10, 7))
        {
            var primeCount = 0;

            for (var i = left; i <= right; i++)
                if (IsPrime(i))
                    primeCount++;

            if (primeCount == M)
                return left;

            left++;
            right++;
        }

        return -1;
    }
    
    private static bool IsPrime(int n)
    {
        switch (n)
        {
            case <= 1:
                return false;
            case 2 or 3:
                return true;
        }

        if (n % 2 == 0 || n % 3 == 0)
            return false;

        var i = 5;
        while (i * i <= n)
        {
            if (n % i == 0 || n % (i + 2) == 0)
                return false;
            i += 6;
        }
        return true;
    }
}