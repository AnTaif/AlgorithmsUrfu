namespace Labs.Utils;

public static class QuickSorter
{
    public static void Sort<T>(T[] array) where T : IComparable<T>
    {
        if (array.Length == 0)
            return;

        Sort(array, (x, y) => x.CompareTo(y));
    }

    public static void Sort<T>(T[] array, Comparison<T> comparison) => Sort(array, 0, array.Length - 1, comparison);
    
    private static void Sort<T>(T[] array, int left, int right, Comparison<T> comparison)
    {
        if (left >= right) return;
        
        var pivotIndex = Partition(array, left, right, comparison);
        Sort(array, left, pivotIndex - 1, comparison);
        Sort(array, pivotIndex + 1, right, comparison);
    }

    private static int Partition<T>(T[] array, int left, int right, Comparison<T> comparison)
    {
        var pivot = array[right];
        var i = left - 1;

        for (var j = left; j < right; j++)
        {
            if (comparison(array[j], pivot) <= 0)
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