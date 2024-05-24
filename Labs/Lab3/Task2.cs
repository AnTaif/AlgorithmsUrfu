namespace Labs.Lab3;

/*
Задача 2. Очередь
Ограничение по времени – 2 секунды.
Ограничение по памяти – 256 мегабайт.
    Реализуйте работу очереди. Для каждой операции изъятия элемента выведите ее
    результат.
    На вход программе подаются строки, содержащие команды. Каждая строка
    содержит одну команду. Команда — это либо «+ N», либо «−». Команда «+ N» означает
    добавление в очередь числа N, по модулю не превышающего 109. Команда «−» означает
    изъятие элемента из очереди. Гарантируется, что размер очереди в процессе выполнения
    команд не превысит 106 элементов.
Формат входного файла
    В первой строке содержится M (1≤M≤106) — число команд. В последующих строках
    содержатся команды, по одной в каждой строке.
Формат выходного файла
    Выведите числа, которые удаляются из очереди с помощью команды «−», по одному
    в каждой строке. Числа нужно выводить в том порядке, в котором они были извлечены из
    очереди. Гарантируется, что извлечения из пустой очереди не производится.
*/

public static class Task2
{
    public static void Run()
    {
        var n = int.Parse(Console.ReadLine()!);

        var commands = new string[n];
        for (var i = 0; i < n; i++)
            commands[i] = Console.ReadLine()!;

        var removedNumbers = Solve(commands);

        foreach (var removedNum in removedNumbers)
            Console.WriteLine(removedNum);
    }

    public static int[] Solve(string[] commandLines)
    {
        var queue = new Queue<int>();
        var removedNumbers = new List<int>();
        
        foreach (var commandLine in commandLines)
        {
            var command = commandLine.Split();

            switch (command[0])
            {
                case "+":
                {
                    var n = int.Parse(command[1]);
                    queue.Enqueue(n);
                    break;
                }
                case "-":
                {
                    var removedNum = queue.Dequeue();
                    removedNumbers.Add(removedNum);
                    break;
                }
            }
        }

        return removedNumbers.ToArray();
    }
}

public class Queue<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;
    public int Count { get; private set; }

    public void Enqueue(T data)
    {
        var newNode = new Node<T>(data);
        Count++;
        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            _tail = newNode;
        }
    }

    public T Dequeue()
    {
        if (_head == null)
            throw new InvalidOperationException("Queue is empty");

        var removedItem = _head.Data;
        _head = _head.Next;

        if (_head == null)
            _tail = null;

        Count--;
        return removedItem;
    }
}

public class Node<T>(T data)
{
    public T Data { get; set; } = data;
    public Node<T>? Next { get; set; }
}