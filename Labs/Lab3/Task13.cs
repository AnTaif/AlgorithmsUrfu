namespace Labs.Lab3;

/*
Задача 13. Постфиксная запись
    В постфиксной записи (или обратной польской записи) операция записывается после
    двух операндов. Например, сумма двух чисел A и B записывается как A B +. Запись B C
    + D * обозначает привычное нам (B + C) * D, а запись A B C + D * + означает A +
    (B + C) * D. Достоинство постфиксной записи в том, что она не требует скобок и
    дополнительных соглашений о приоритете операторов для своего чтения.
    Дано выражение в обратной польской записи. Определите его значение.
Формат входных данных:
    В первой строке входного файла дано число N (1 <= N <= 10^6) - число элементов
    выражения. Во второй строке содержится выражение в постфиксной записи, состоящее
    из N элементов. В выражении могут содержаться неотрицательные однозначные числа и
    операции +, -, *. Каждые два соседних элемента выражения разделены ровно одним
    пробелом.
Формат выходных данных:
    Необходимо вывести значение записанного выражения. Гарантируется, что
    результат выражения, а также результаты всех промежуточных вычислений, по модулю
    будут меньше, чем 231.
*/

public static class Task13
{
    public static void Run()
    {
        _ = int.Parse(Console.ReadLine()!);
        var postfixInput = Console.ReadLine()!;

        var result = Solve(postfixInput);
        
        Console.WriteLine(result);
    }

    public static int Solve(string input)
    {
        var stack = new Stack<int>();

        var elements = input.Split();

        foreach (var element in elements)
        {
            var isNumeric = int.TryParse(element, out var num);
            
            if (!isNumeric)
            {
                var n2 = stack.Pop();
                var n1 = stack.Pop();

                num = char.Parse(element) switch
                {
                    '+' => n1 + n2,
                    '-' => n1 - n2,
                    '*' => n1 * n2,
                    _ => throw new ArgumentException("Unknown operation: " + element)
                };
            }

            stack.Push(num);
        }

        return stack.Pop();
    }
}

public class Stack<T>(int capacity = 100)
{
    private int _topIndex = -1;
    private readonly T[] _stack = new T[capacity];

    public bool IsEmpty => _topIndex == -1;

    public void Push(T data) 
    { 
        if (_topIndex >= capacity - 1) 
            throw new InvalidOperationException("Stack Overflow"); 
            
        _stack[++_topIndex] = data; 
    } 
  
    public T Pop() 
    { 
        if (_topIndex < 0) 
            throw new InvalidOperationException("Stack Underflow"); 

        var value = _stack[_topIndex--]; 
        return value; 
    }
}