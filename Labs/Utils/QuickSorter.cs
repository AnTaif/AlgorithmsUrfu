namespace Labs.Utils;

public static class QuickSorter
{
    public static void Sort<T>(T[] array) where T : IComparable<T>
    {
        if (array.Length == 0)
            return;

        Sort(array, Comparer<T>.Default);
    }

    private static void Sort<T>(T[] array, Comparer<T> comparer) => Sort(array, 0, array.Length - 1, comparer);
    
    private static void Sort<T>(T[] array, int left, int right, Comparer<T> comparer)
    {
        if (left >= right) return;
        
        var pivotIndex = Partition(array, left, right, comparer);
        Sort(array, left, pivotIndex - 1, comparer);
        Sort(array, pivotIndex + 1, right, comparer);
    }

    private static int Partition<T>(T[] array, int left, int right, Comparer<T> comparer)
    {
        var pivot = array[right];
        var i = left - 1;

        for (var j = left; j < right; j++)
        {
            if (comparer.Compare(array[j], pivot) <= 0)
            {
                i++;
                Swap(array, i, j);
            }
        }

        Swap(array, i + 1, right);
        return i + 1;
    }

    private static void Swap<T>(T[] array, int i, int j) => (array[i], array[j]) = (array[j], array[i]);
}