namespace Labs.Lab7;

/*
Задача 3. Наибольшая общая подстрока
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайта
    Во входном файле находятся две строки, длиной до 30000 символов, состоящих из
    цифр и прописных и строчных букв латинского алфавита, каждая в отдельной строке файла.
    Необходимо найти общую подстроку наибольшей длины. Если таких подстрок
    несколько, то следует вывести ту из них, которая лексикографически меньше.
    Обратите внимание, что в приведенном примере имеется две подстроки длины 4 —
    rash и abra. Несмотря на то, что первая встречается раньше, ответом будет вторая, так как
    она лексикографически меньше.
*/

public static class Task3
{
    public static void Run()
    {
        var line1 = Console.ReadLine()!;
        var line2 = Console.ReadLine()!;

        var result = Solve(line1, line2);
        
        Console.WriteLine(result);
    }

    public static string Solve(string line1, string line2)
    {
        var dp = new int[line1.Length + 1, line2.Length + 1];
        var maxLength = 0;
        var endIndex = 0;

        for (var i = 1; i <= line1.Length; i++)
        {
            for (var j = 1; j <= line2.Length; j++)
            {
                if (line1[i - 1] != line2[j - 1]) continue;
                
                dp[i, j] = dp[i - 1, j - 1] + 1;

                if (dp[i, j] > maxLength)
                {
                    maxLength = dp[i, j];
                    endIndex = i - 1;
                }
                else if (dp[i, j] == maxLength && 
                         Compare(line1, i - maxLength, endIndex - maxLength + 1, maxLength) < 0)
                {
                    endIndex = i - 1;
                }
            }
        }

        return line1.Substring(endIndex - maxLength + 1, maxLength);
    }

    private static int Compare(string line, int firstStart, int secondStart, int length) =>
        string.CompareOrdinal(line.Substring(firstStart, length), line.Substring(secondStart, length));
}