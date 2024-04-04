namespace Labs.Lab3;

/*
Задача 4. Очередь с приоритетами
    Реализуйте очередь с приоритетами. Ваша очередь должна поддерживать
    следующие операции: добавить элемент, извлечь минимальный элемент, уменьшить
    элемент, добавленный во время одной из операций.
Формат входных данных:
    В первой строке входного файла содержится число n (1≤n≤106) - число операций с
    очередью.
    Следующие n строк содержат описание операций с очередью, по одному описанию
    в строке. Операции могут быть следующими:
    A x — требуется добавить элемент x в очередь.
    X — требуется удалить из очереди минимальный элемент и вывести его в выходной
    файл. Если очередь пуста, в выходной файл требуется вывести звездочку «*».
    D x y — требуется заменить значение элемента, добавленного в очередь
    операцией A в строке входного файла номер x+1, на y. Гарантируется, что в
    строке x+1 действительно находится операция A, что этот элемент не был ранее удален
    операцией X, и что y меньше, чем предыдущее значение этого элемента.
    В очередь помещаются и извлекаются только целые числа, не превышающие по
    модулю 109.
Формат выходных данных:
    Выведите последовательно результат выполнения всех операций X, по одному в
    каждой строке выходного файла. Если перед очередной операцией X очередь пуста,
    выведите вместо числа звездочку «*».
*/

public static class Task4
{
    public static void Run()
    {
        var n = int.Parse(Console.ReadLine()!);
        
        var operations = new string[n];

        for (var i = 0; i < n; i++)
            operations[i] = Console.ReadLine()!;

        var removedValues = Solve(operations);

        foreach (var removedValue in removedValues)
            Console.WriteLine(removedValue);
    }

    public static string[] Solve(string[] operationLines)
    {
        var queue = new PriorityQueue();
        var removedValues = new List<string>();
        
        foreach (var operationLine in operationLines)
        {
            var operation = operationLine.Split();
            
            switch (operation[0])
            {
                case "A":
                {
                    var x = int.Parse(operation[1]);
                    queue.Enqueue(x);
                    break;
                }
                case "X":
                {
                    if (queue.IsEmpty())
                        removedValues.Add("*");
                    else
                        removedValues.Add(queue.DequeueMin().ToString());
                    break;
                }
                case "D":
                {
                    var x = int.Parse(operation[1]);
                    var y = int.Parse(operation[2]);
                    queue.DecreaseKey(x, y);
                    break;
                }
            } 
        }

        return removedValues.ToArray();
    }
}

public class PriorityQueue
{
    private readonly List<int> _heap = new(); // корень - 0 элемент, левый потомок - 2*i+1, правый - 2*i+2
    public int Count => _heap.Count;

    public void Enqueue(int value)
    {
        _heap.Add(value);
        HeapifyUp();
    }

    public int DequeueMin()
    {
        if (Count == 0) throw new InvalidOperationException("Queue is empty");

        var min = _heap[0];
        _heap[0] = _heap.Last();
        _heap.RemoveAt(Count-1);

        HeapifyDown();
        
        return min;
    }

    public void DecreaseKey(int index, int value)
    {
        _heap[index] = value;
        HeapifyUp(index);
    }
    
    private void HeapifyUp(int index = -1)
    {
        if (index == -1)
            index = Count - 1;
        
        var parent = (index - 1) / 2;

        while (index > 0 && _heap[parent] > _heap[index])
        {
            (_heap[index], _heap[parent]) = (_heap[parent], _heap[index]);

            index = parent;
            parent = (index - 1) / 2;
        }
    }

    private void HeapifyDown(int index = 0)
    {
        while(true)
        {
            var leftIndex = 2 * index + 1;
            var rightIndex = 2 * index + 2;
            var minChildIndex = index;

            if (leftIndex < Count && _heap[leftIndex] < _heap[minChildIndex]) 
                minChildIndex = leftIndex;

            if (rightIndex < Count && _heap[rightIndex] < _heap[minChildIndex])
                minChildIndex = rightIndex;

            if (minChildIndex == index) 
                break;

            (_heap[index], _heap[minChildIndex]) = (_heap[minChildIndex], _heap[index]);
            index = minChildIndex;
        }
    }
    
    public bool IsEmpty() => _heap.Count == 0;
}