using Labs.Utils;

namespace Labs.Lab4;

/*
Задача 4. Анаграммы
Ограничение по времени: 0.5 секунд
Ограничение по памяти: 256 мегабайт
    Как известно, анаграммами называются слова, которые могут получиться друг из
    друга путем перестановки букв, например LOOP, POOL, POLO. Будем называть все слова
    такого рода комплектом.
    На вход программы подается число слов 1 ≤ N ≤ 100000. В каждой из очередных N
    строк присутствует одно слово, состоящее из заглавных букв латинского алфавита. Все
    слова имеют одинаковую длину 3 ≤ L ≤ 10000.
    Требуется определить число комплектов во входном множестве.
Формат входных данных:
    N
    W1
    W2
    …
    WN
Формат выходных данных:
    NumberOfComplects 
*/

public static class Task4
{
    public static void Run()
    {
        var n = int.Parse(Console.ReadLine()!);

        var words = new string[n];
        for (var i = 0; i < n; i++)
            words[i] = Console.ReadLine()!;

        var count = Solve(words);

        Console.WriteLine(count);
    }

    public static int Solve(string[] words)
    {
        var trie = new AnagramTrie();

        foreach (var word in words)
        {
            var characters = word.ToCharArray();
            QuickSorter.Sort(characters);
            trie.Insert(new string(characters));
        }
        
        return trie.GetComplectsNumber();
    }
}

// Префиксное дерево
class AnagramTrie
{
    private readonly AnagramNode root = new();
    private int complectsNumber;

    public void Insert(string word)
    {
        var current = root;
        foreach (var c in word)
        {
            current.Children.TryAdd(c, new AnagramNode());
            current = current.Children[c];
        }
        
        if (!current.IsEndOfWord)
        {
            complectsNumber++;
            current.IsEndOfWord = true;
        }
    }

    public int GetComplectsNumber() => complectsNumber;
}

class AnagramNode
{
    public Dictionary<char, AnagramNode> Children { get; } = new();
    public bool IsEndOfWord { get; set; }
}