namespace Labs.Lab1;

/*
Задача 2. Два массива.
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайта
    Даны два упорядоченных по неубыванию массива. Требуется найти количество
    таких элементов, которые присутствуют в обоих массивах. Например, в массивах (0, 0, 1, 1,
    2, 3) и (0, 1, 1, 2) имеется четыре общих элемента – (0, 1, 1, 2).
    Первая строка содержит размеры массивов N1 и N2. В следующих N1 строках
    содержатся элементы первого массива, в следующих за ними N2 строках – элементы
    второго массива.
    Программа должна вывести ровно одно число – количество общих элементов.
Формат входных данных:
    Na, Nb
    a1
    a2
    …
    aNa
    b1
    b2
    …
    bNb
Формат выходных данных:
    Одно целое число – количество общих элементов
*/

public static class Task2
{
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var a = int.Parse(input[0]);
        var b = int.Parse(input[1]);

        var arrA = InputArray(a);
        var arrB = InputArray(b);

        var result = CountCommonElements(arrA, arrB);

        Console.WriteLine(result);
    }

    public static int CountCommonElements(int[] arrA, int[] arrB)
    {
        var count = 0;
        var i = 0; // указатель 1 массива
        var j = 0; // указатель 2 массива

        while (i < arrA.Length && j < arrB.Length)
        {
            var a = arrA[i];
            var b = arrB[j];

            if (a == b)
            {
                count++;
                i++;
                j++;
            }
            else if (a > b) 
                j++;
            else 
                i++;
        }

        return count;
    }

    private static int[] InputArray(int size)
    {
        var arr = new int[size];
        for (var i = 0; i < size; i++)
            arr[i] = int.Parse(Console.ReadLine()!);
        
        return arr;
    }
}