namespace Labs.Lab1;

/*
Задача 4. Вычисление полинома.
Ограничение по времени: 1 секунда
Ограничение по памяти: 16 мегабайт
    Вычисление полинома – необходимая операция для многих алгоритмов. Нужно
    вычислить значение полинома
    AnX^n + A(n-1)X^(n-1) + ... + A2X^2 + A1X^1 + A0
    Так как число n может быть достаточно велико, требуется вычислить значение
    полинома по модулю M. Сделать это предлагается для нескольких значений аргумента.
Формат входных данных:
    Первая строка файла содержит три числа – степень полинома 2 ≤ N ≤ 100000,
    количеств вычисляемых значений аргумента 1 ≤ M ≤ 10000 и модуль 10 ≤ MOD ≤ 10^9.
    Следующие N+1 строк содержат значения коэффициентов полинома 0 ≤ ai ≤ 10^9
    В очередных M строках содержатся значения аргументов 0 ≤ xi ≤ 10^9.
Формат выходных данных:
    Выходной файл должен состоять из ровно M строк – значений данного полинома
    при заданных значениях аргументов по модулю MOD.
*/

public static class Task4
{
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var n = int.Parse(input[0]); // степень полинома
        var m = int.Parse(input[1]); // кол-во вычисляемых значений аргумента x
        var mod = int.Parse(input[2]); // модуль

        var arrA = InputArray(n + 1); // коэффициенты a
        var arrX = InputArray(m); // значения аргумента x

        var result = SolveArray(arrA, arrX, mod);

        Console.WriteLine(string.Join(" ", result));
    }

    public static int[] SolveArray(int[] arrA, int[] arrX, int mod)
    {
        var result = new int[arrX.Length];

        for (var i = 0; i < arrX.Length; i++)
            result[i] = SolveEquation(arrA, arrX[i], mod);

        return result;
    }

    private static int SolveEquation(int[] arrA, int x, int mod)
    {
        var result = 0;

        for (var i = 0; i < arrA.Length; i++)
        {
            var a = arrA[i];

            var power = arrA.Length - 1 - i;
            result += a * BinaryPower(x, power) % mod;
        }
        return result % mod;
    }

    // O(logN) - сложность алгоритма бинарного возведения в степень
    private static int BinaryPower(int baseNum, int exp)
    {
        var result = 1;

        while (exp != 0)
        {
            if (exp % 2 == 1)
                result *= baseNum;

            baseNum *= baseNum;
            exp /= 2;
        }

        return result;
    }

    private static int[] InputArray(int size)
    {
        var arr = new int[size];
        for (var i = 0; i < size; i++)
            arr[i] = int.Parse(Console.ReadLine()!);
        
        return arr;
    }
}