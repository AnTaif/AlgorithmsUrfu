using System.Numerics;

namespace Labs.Lab5;

/*
Задача 4. Точные квадраты
Ограничение по времени: 1 секунды
Ограничение по памяти: 256 мегабайта
	Можете ли вы по десятичному представлению натурального числа определить, является ли это число полным квадратом?
	А если в числе много десятичных знаков?
Формат входных данных	
	Первая строка содержит 5 ≤ N ≤ 106 — количество тех чисел, которые нужно проверить. В последующих N строках —
	десятичные представления натуральных чисел количеством десятичных цифр в представлении не более 100.
Формат выходных данных
	Для каждого из чисел, являющихся полным квадратом, вывести его номер. Нумерация начинается с единицы.
*/

public class Task4
{
    public static void Run()
    {
	    var N = int.Parse(Console.ReadLine()!);
	    var numbers = new string[N];

	    for (var i = 0; i < N; i++)
		    numbers[i] = Console.ReadLine()!;

	    var positions = Solve(numbers);

	    Console.WriteLine("\nOUTPUT:" + (positions.Count == 0 ? " empty" : ""));
	    foreach (var pos in positions)
		    Console.WriteLine(pos);
    }

    public static List<int> Solve(string[] numbers)
    {
	    var positions = new List<int>();

	    for (var i = 0; i < numbers.Length; i++)
	    {
		    if (IsPerfectSquare(numbers[i]))
				positions.Add(i + 1);
	    }

	    return positions;
    }

    private static bool IsPerfectSquare(string number)
    {
	    var num = BigInteger.Parse(number);
	    var sqrt = BinarySqrt(num);

	    return sqrt * sqrt == num;
    }

    private static BigInteger BinarySqrt(BigInteger x)
    {
	    if (x == 0 || x == 1)
		    return x;

	    BigInteger start = 1, end = x, ans = 0;
	    while (start <= end)
	    {
		    var mid = (start + end) / 2;
		    if (mid * mid == x)
			    return mid;
		    if (mid * mid < x)
		    {
			    start = mid + 1;
			    ans = mid;
		    }
		    else
			    end = mid - 1;
	    }
	    return ans;
    }
}