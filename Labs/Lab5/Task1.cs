namespace Labs.Lab5;

/*
Задача 1. Подстроки
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайта
	Входной файл состоит из одной строки I, содержащей малые буквы английского алфавита.
	Назовем подстроковой длиной L с началом S множество непрерывно следующих символов строки.
	Например, строка
	abcab
	содержит подстроки:
	длины 1: a, b, c, a, b
	длины 2: ab, bc, ca, ab
	длины 3: abc, bca, cab
	длины 4: abca, bcab
	длины 5: abcab
	В строках длины 1 есть два повторяющихся элемента — a и b. 
	Назовем весом подстрок длины L произведение максимального количества повторяющихся подстрок этой длины на длину L.
	В нашем случае вес длины 1 есть 2 (2*1), длины 2 есть 4 (2*2), длины 3 — 1 (1*3), длины 4 — 1 и длины 5 — 1.
	Требуется найти наибольший из всех весов различных длин.
*/

public static class Task1
{
	public static void Run()
	{
		var input = Console.ReadLine()!;
		
		var maxWeight = Solve(input);

		Console.WriteLine(maxWeight);
	}

	public static int Solve(string input)
	{
		var substrings = new Dictionary<string, int>();
		var weights = new Dictionary<int, int>();
		
		for (var length = 1; length <= input.Length; length++)
		{
			for (var start = 0; start <= input.Length - length; start++)
			{
				var substring = input.Substring(start, length);
				if (substrings.ContainsKey(substring))
				{
					substrings[substring]++;
				}
				else
					substrings[substring] = 1;

				if (weights.TryGetValue(length, out var value))
				{
					weights[length] = Math.Max(value, substrings[substring]);
				}
				else
					weights[length] = substrings[substring];
			}
		}
		
		var maxWeight = 0;
		foreach (var pair in weights)
			maxWeight = Math.Max(maxWeight, pair.Key * pair.Value);
		
		return maxWeight;
	}
}