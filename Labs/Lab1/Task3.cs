using System.Text;

namespace Labs.Lab1;

/*
Задача 3. Длинное сложение и вычитание
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайта
    На вход подается три строки. Первая содержит представление длинного десятичного
    числа (первый операнд), вторая – представление операции, строки + и -, третья –
    представление второго операнда.
    Длина первой и третьей строки ограничены 1000 символами. Вторая строка
    содержит ровно один символ.
    Требуется исполнить операцию и вывести результат в десятичном представлении.
Формат входных данных:
    123
    +
    999
Формат выходных данных:
    1122
*/

public static class Task3
{
    public static void Run()
    {
        var num1 = Console.ReadLine()!;
        var op = Console.ReadLine()![0];
        var num2 = Console.ReadLine()!;

        var i = new BigInt(num1);
        var j = new BigInt(num2);

        var result = Solve(i, j, op);

        Console.WriteLine(result.Value);
    }

    public static BigInt Solve(BigInt i, BigInt j, char op)
    {
        return op switch
        {
            '+' => i.Plus(j),
            '-' => i.Minus(j),
            _ => throw new ArgumentException("Unsupported operation")
        };
    }
}

public class BigInt
{
    private const int Base = 9; // Размер разряда (9 - максимальный для int32)
    private const int BaseNum = 1000000000; // Минимальное число большее BASE (10^BASE)

    private bool IsNegative { get; set; }

    public string Value { get; private set; }

    private readonly int[] _digits;

    public BigInt(string str)
    {
        Value = str;

        switch (str[0])
        {
            case '-':
                IsNegative = true;
                str = str[1..];
                break;
            case '+':
                str = str[1..];
                break;
        }

        var size = (int)Math.Ceiling((double)str.Length / Base);
        _digits = new int[size];

        var currEndIndex = str.Length;

        for (var i = 0; i < _digits.Length; i++)
        {
            var nextEndIndex = currEndIndex - Base;

            if (nextEndIndex < 0)
                nextEndIndex = 0;

            var currNumber = int.Parse(str.Substring(nextEndIndex, currEndIndex - nextEndIndex));
            _digits[i] = currNumber;

            currEndIndex = nextEndIndex;
        }
    }

    public BigInt Plus(BigInt other)
    {
        if (IsNegative == other.IsNegative)
            return PlusBase(other);
        
        return IsNegative ? other.MinusBase(this) : MinusBase(other);
    }

    public BigInt Minus(BigInt other)
    {
        if (IsNegative && other.IsNegative)
            return other.MinusBase(this);
        if (IsNegative || other.IsNegative)
            return PlusBase(other);

        return MinusBase(other);
    }

    private BigInt PlusBase(BigInt other)
    {
        var maxLength = Math.Max(_digits.Length, other._digits.Length);
        var result = new int[maxLength + 1];

        for (var i = 0; i < maxLength; i++)
        {
            var sum = 0;

            if (i < _digits.Length)
                sum += _digits[i];
            if (i < other._digits.Length)
                sum += other._digits[i];

            result[i] += sum;

            if (result[i] < BaseNum) continue;
            
            result[i] -= BaseNum;
            result[i + 1]++;
        }

        return new BigInt(DigitsToString(result, IsNegative));
    }

    private BigInt MinusBase(BigInt other)
    {
        if (IsThisSmaller(other))
        {
            other.Negate();
            return other.MinusBase(this);
        }

        var maxLength = Math.Max(_digits.Length, other._digits.Length);
        var result = new int[maxLength + 1];
        var isResultNegative = IsNegative;

        for (var i = 0; i < maxLength; i++)
        {
            var diff = 0;

            if (i < _digits.Length)
                diff += _digits[i];
            if (i < other._digits.Length)
                diff -= other._digits[i];

            result[i] += diff;
            
            if (result[i] >= 0) continue;
            
            if (i + 1 == maxLength)
            {
                result[i] = Math.Abs(result[i]);
                isResultNegative = true;
                continue;
            }

            result[i] += BaseNum;
            result[i + 1] = -1;
        }

        return new BigInt(DigitsToString(result, isResultNegative));
    }

    private bool IsThisSmaller(BigInt other)
    {
        if (_digits.Length > other._digits.Length)
            return false;
        if (_digits.Length < other._digits.Length)
            return true;

        for (var i = _digits.Length - 1; i >= 0; i--)
        {
            var diff = _digits[i] - other._digits[i];
            if (diff < 0)
                return true;
            if (diff > 0)
                return false;
        }

        return false;
    }

    private void Negate()
    {
        if (IsNegative)
            return;
        IsNegative = true;
        Value = "-" + Value;
    }

    private static string DigitsToString(IReadOnlyList<int> digits, bool isNegative)
    {
        var i = digits.Count - 1;

        while (i >= 0 && digits[i] == 0)
            i--;

        if (i == -1)
            return "0";

        var sb = new StringBuilder();

        if (isNegative)
            sb.Append('-');

        for (; i >= 0; i--)
        {
            if (digits[i] == 0)
            {
                sb.Append("0".PadLeft(Base, '0'));
                continue;
            }
            sb.Append(digits[i]);
        }

        return sb.ToString();
    }

    public string ToString() => Value;
}