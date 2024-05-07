namespace Labs.Lab6;

/*
Задача 1. Сумма элементов подмассива
Ограничение по времени: 1 секунда
Ограничение по памяти: 256 мегабайт
    Имеется массив V целых чисел, состоящий из 1 ≤ N ≤ 10^8 элементов, -2 × 10^9 ≤ Vi ≤ 2 × 10^9.
    Подмассивом называют непрерывное подмножество элементов массива, возможно, включающее в себя и полный массив.
    Требуется найти наибольшую из возможных сумм всех подмассивов.
Формат входных данных:
    N
    V1
    V2
    …
    VN
Формат выходных данных:
    MaximalSubarraysSum
*/

public static class Task1
{
    public static void Run()
    {
        var N = int.Parse(Console.ReadLine()!);
        var V = new int[N];
        for (var i = 0; i < N; i++)
            V[i] = int.Parse(Console.ReadLine()!);

        var result = Solve(V);
        
        Console.WriteLine(result);
    }

    public static int Solve(int[] V)
    {
        var maxSum = V[0];
        var currentSum = V[0];

        for (var i = 1; i < V.Length; i++)
        {
            // Находим максимальную сумму подмассива, заканчивающегося на текущем элементе
            currentSum = Math.Max(V[i], currentSum + V[i]);
            // Обновляем максимальную сумму
            maxSum = Math.Max(maxSum, currentSum);
        }

        return maxSum;
    }
}