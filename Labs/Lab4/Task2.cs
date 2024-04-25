namespace Labs.Lab4;

/*
Задача 2. Поиск множеств
Ограничение по времени: 1 секунда
Ограничение по памяти: 64 мегабайта
    В первой строке файла содержится три числа: N – количество эталонных множеств,
    M – размер каждого из множеств и K – количество пробных множеств.
    Каждое из множеств содержит целые числа от 0 до 10^9, числа могут повторяться.
    Требуется для каждого из пробных множеств вывести в отдельной строке цифру ‘1’,
    если это множество в точности совпадает с каким-либо из эталонных множеств и цифру ‘0’,
    если оно ни с одним не совпадает, то есть выведено должно быть в точности K строк.
    5 ≤ N ≤ 50000
    3 ≤ M ≤ 1000
    5 ≤ K ≤ 50000
*/

public static class Task2
{
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var N = int.Parse(input[0]);
        _ = int.Parse(input[1]);
        var K = int.Parse(input[2]);

        var sets = new string[N];
        for (var i = 0; i < N; i++)
            sets[i] = Console.ReadLine()!;

        var trialSets = new string[K];
        
        for (var i = 0; i < K; i++)
            trialSets[i] = Console.ReadLine()!;

        var result = Solve(sets, trialSets);
        
        Console.WriteLine();
        foreach(var r in result)
            Console.WriteLine(r);
    }

    public static int[] Solve(string[] sets, string[] trialSets)
    {
        var trie = new Trie();
        
        // Вставка эталонных множеств
        foreach (var s in sets)
        {
            var set = s.Split().Select(int.Parse).ToList();
            set.Sort();
            
            trie.Insert(set.ToArray());
        }

        // Поиск пробных множеств
        var results = new int[trialSets.Length];
        for (var i = 0; i < trialSets.Length; i++)
        {
            var set = trialSets[i].Split().Select(int.Parse).ToList();
            set.Sort();

            results[i] = trie.Search(set.ToArray()) ? 1 : 0;
        }

        return results;
    }
}

class Trie
{
    private readonly TrieNode _root = new();

    public void Insert(int[] set)
    {
        var current = _root;
        foreach (var num in set)
        {
            current.Children.TryAdd(num, new TrieNode());
            current = current.Children[num];
        }
        current.IsEnd = true;
    }

    public bool Search(int[] set)
    {
        var current = _root;
        foreach (var num in set)
        {
            if (!current.Children.ContainsKey(num))
                return false;
            
            current = current.Children[num];
        }
        return current.IsEnd;
    }
}

class TrieNode
{
    public Dictionary<int, TrieNode> Children { get; } = new();
    public bool IsEnd { get; set; }
}