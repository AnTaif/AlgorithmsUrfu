namespace Labs.Lab4;

/*
Задача 1. Запросы сумм
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайта
    В первой строке файла содержатся два числа: количество элементов в массиве V: 10
    ≤ N ≤ 500000 и количество запросов 1 ≤ M ≤ 500000. Каждый элемент массива лежит в
    интервале [0…232).
    Каждый запрос – отдельная строка, состоящая из кода запроса, который может быть
    равен 1 или 2 и аргументов запроса.
    Запрос с кодом один содержит два аргумента, начало L и конец отрезка R массива.
    В ответ на этот запрос программа должна вывести значения суммы элементов массива от
    V[L] до V[R] включительно.
    Запрос с кодом два содержит тоже два аргумента, первый из которых есть номер
    элемента массива V, а второй – его новое значение.
    Количество выведенных строк должно совпадать с количеством запросов первого
    типа
*/

public static class Task1
{
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var N = int.Parse(input[0]);
        var M = int.Parse(input[1]);

        var V = new int[N];

        for (var i = 0; i < N; i++)
            V[i] = int.Parse(Console.ReadLine()!);

        var queries = new string[M];
        for (var i = 0; i < M; i++)
            queries[i] = Console.ReadLine()!;

        var result = Solve(V, queries);

        Console.WriteLine("\nСуммы:");
        foreach (var res in result)
            Console.WriteLine(res);
    }

    public static int[] Solve(int[] V, string[] queries)
    {
        var tree = new SegmentTree(V);

        var sums = new List<int>();
        
        foreach (var queryLine in queries)
        {
            var query = queryLine.Split();
            var code = int.Parse(query[0]);
            var arg1 = int.Parse(query[1]);
            var arg2 = int.Parse(query[2]);
            
            switch (code)
            {
                case 1:
                {
                    sums.Add(tree.Sum(arg1, arg2));
                    break;
                }
                case 2:
                {
                    tree.Update(arg1, arg2);
                    V[arg1] = arg2;
                    break;
                }
                default: 
                    throw new ArgumentException("Неизвестный код запроса: " + code);
            }
        }

        return sums.ToArray();
    }
}

// Дерево отрезков
class SegmentTree
{
    private readonly int[] _array;
    private readonly int[] _tree;

    public SegmentTree(int[] array)
    {
        _array = array;
        
        var height = (int)Math.Ceiling(Math.Log(_array.Length, 2));
        var maxSize = 2 * (int)Math.Pow(2, height) - 1;
        _tree = new int[maxSize];

        BuildTree(0, 0, _array.Length - 1);
    }

    private void BuildTree(int v, int start, int end)
    {
        if (start == end)
            _tree[v] = _array[start];
        else
        {
            var mid = (start + end) / 2;
            
            BuildTree(v * 2 + 1, start, mid);
            BuildTree(v * 2 + 2, mid + 1, end);
    
            _tree[v] = _tree[v * 2 + 1] + _tree[v * 2 + 2];
        }
    }

    public int Sum(int left, int right) => Sum(0, 0, _array.Length - 1, left, right);
    
    private int Sum(int index, int start, int end, int left, int right)
    {
        if (left <= start && right >= end)
            return _tree[index];
        if (end < left || start > right)
            return 0;

        var mid = (start + end) / 2;
        return Sum(index * 2 + 1, start, mid, left, right) +
               Sum(index * 2 + 2, mid + 1, end, left, right);
    }

    public void Update(int updateIndex, int newValue) =>
        Update(0, 0, _array.Length - 1, updateIndex, newValue - _array[updateIndex]);
    
    private void Update(int index, int start, int end, int updateIndex, int diff)
    {
        if (updateIndex < start || updateIndex > end)
            return;
        
        _tree[index] += diff;
        if (start != end)
        {
            var mid = (start + end) / 2;
            Update(2 * index + 1, start, mid, updateIndex, diff);
            Update(2 * index + 2, mid + 1, end, updateIndex, diff);
        }
    }
}