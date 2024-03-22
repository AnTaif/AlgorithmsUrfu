namespace Labs.Lab1;

/*
Задача 6. Две кучи.
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайт
    Имеется 2 ≤ N ≤ 23 камня с целочисленными весами W1, W2, … WN. Требуется
    разложить их на две кучи таким образом, чтобы разница в весе куч была минимальной.
    Каждый камень должен принадлежать ровно одной куче.
Формат входных данных:
    N
    W1 W2 W3 … WN
Формат выходных данных:
    Минимальная неотрицательная разница в весе куч
*/

public static class Task6
{
    public static void Run()
    {
        var n = int.Parse(Console.ReadLine()!);
        var arr = InputArray(n);

        var result = FindMinDifference(arr);
        Console.WriteLine(result);
    }

    public static int FindMinDifference(int[] arr)
    {
        QuickSorter.QuickSort(arr, 0, arr.Length - 1);

        var left = 1;
        var right = arr.Length - 1;
        var firstHeap = arr[0];
        var secondHeap = arr[right];

        while (left != right)
        {
            firstHeap += arr[left];

            if (firstHeap >= secondHeap)
            {
                right--;
                if (left == right)
                    break;
                secondHeap += arr[right];
            }

            left++;
        }

        return Math.Abs(firstHeap - secondHeap);
    }

    private static int[] InputArray(int size)
    {
        var arr = new int[size];
        for (var i = 0; i < size; i++)
        {
            arr[i] = int.Parse(Console.ReadLine()!);
        }
        return arr;
    }
}

public static class QuickSorter
{
    public static void QuickSort(int[] arr, int low, int high)
    {
        if (low >= high) return;
        
        var pi = Partition(arr, low, high);
        QuickSort(arr, low, pi - 1);
        QuickSort(arr, pi + 1, high);
    }

    private static int Partition(int[] arr, int low, int high)
    {
        var pivot = arr[high];
        var i = low - 1;

        for (var j = low; j <= high - 1; j++)
        {
            if (arr[j] >= pivot) continue;
            
            i++;
            Swap(arr, i, j);
        }
        Swap(arr, i + 1, high);
        return i + 1;
    }
    
    private static void Swap(int[] arr, int i, int j)
    {
        (arr[i], arr[j]) = (arr[j], arr[i]);
    }
}