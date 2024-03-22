namespace Labs.Utils;

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