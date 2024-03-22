namespace Labs.Lab2;

/*
Задача 4. Очень быстрая сортировка.
Ограничение по времени: 1.5 секунд
Ограничение по памяти: 512 мегабайт
    Имеется рекуррентная последовательность A1, A2, …, AN, строящаяся по
    следующему правилу:
    A1 = K
    Ai+1 = (Ai * M) % (2^32 – 1) % L
    Требуется найти сумму всех нечетных по порядку элементов в отсортированной по
    неубыванию последовательности по модулю L.
    Для входных данных
    5 7 13 100
    последовательность будет такой:
    {7; 7 * 13%100 = 91; 91 * 13%100 = 83; 83 * 13%100 = 79; 79 * 13%100 = 27}, то есть
    {10; 91; 83; 79; 27}.
    Отсортированная последовательность {7; 27; 79; 83; 91}.
    Сумма элементов на нечетных местах = (7 + 79 + 91)%100 = 77.
Формат входных данных:
    N K M L
    5000000 ≤ N ≤ 60000000, 0 ≤ K, L, M ≤ 2^32 – 1
Формат выходных данных:
    RESULT
*/

public static class Task4
{
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var n = int.Parse(input[0]);
        var k = int.Parse(input[1]);
        var m = int.Parse(input[2]);
        var l = int.Parse(input[3]);

        var arr = GenerateArray(n, k, m, l);
        var sortedArr = CountingSort(arr);
        var sum = OddSum(sortedArr, l);
        
        Console.WriteLine(string.Join(" ", arr));
        Console.WriteLine(string.Join(" ", sortedArr));
        Console.WriteLine(sum);
    }

    public static int OddSum(int[] sortedArr, int l)
    {
        var sum = 0;
        for (var i = 0; i < sortedArr.Length; i += 2)
        {
            sum += sortedArr[i];
        }
        return sum % l;
    }

    public static int[] CountingSort(int[] arr)
    {
        var n = arr.Length;
        
        var max = 0;
        foreach (var a in arr)
            max = Math.Max(max, a);

        var countArray = new int[max + 1];
        var sortedArray = new int[n];

        foreach (var a in arr)
            countArray[a]++;

        var index = 0;
        for (var i = 0; i < max + 1; i++)
            for (var j = 0; j < countArray[i]; j++)
                sortedArray[index++] = i;

        return sortedArray;
    }

    public static int[] GenerateArray(int n, int k, int m, int l)
    {
        var a = new int[n];
        a[0] = k;
        for (var i = 0; i < n - 1; i++)
            a[i + 1] = (int)(((ulong)a[i] * (ulong)m) & 0xFFFFFFFFU) % l;
        return a;
    }
}