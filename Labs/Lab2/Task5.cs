namespace Labs.Lab2;

/*
Задача 5. Внешняя сортировка.
    Ограничение по времени: 2 секунды
    Ограничение по памяти: 2 мегабайта
    В файле «input.txt» содержатся строки символов, длина каждой строки не превышает
    10000 байт. Файл нужно отсортировать в лексикографическом порядке и вывести результат
    в файл «output.txt». Вот беда, файл занимает много мегабайт, а в Вашем распоряжении
    оказывается вычислительная система с очень маленькой оперативной памятью. Но файл
    должен быть отсортирован!
input.txt
    qwertyuiopasdffghhj
    qpoiuytredgfhfd
    asdfghjjklvcvx
    alkjghcdysdfgsr
    pquytrgsdjdsa
    akjhfgdghshhfuushvdfs
output.txt
    akjhfgdghshhfuushvdfs
    alkjghcdysdfgsr
    asdfghjjklvcvx
    pquytrgsdjdsa
    qpoiuytredgfhfd
    qwertyuiopasdffghhj
*/

public static class Task5
{
    private static string rootPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName;
    
    public static void Run()
    {
        var inputPath = Path.Combine(rootPath, "Lab2/input5.txt");
        var outputPath = Path.Combine(rootPath, "Lab2/output5.txt");
        
        ExternalSort.ExternalMergeSort(inputPath, outputPath);
    }
}

public static class ExternalSort
{
    private const int BufferSize = 1024 * 1024; // 1MB

    public static void ExternalMergeSort(string inputFile, string outputFile)
    {
        var chunkCount = SplitFileIntoChunks(inputFile);

        MergeChunks(outputFile, chunkCount);

        DeleteChunkFiles(chunkCount);
    }

    private static int SplitFileIntoChunks(string inputFile)
    {
        var chunkSize = 0;
        var chunkIndex = 0;
        var buffer = new List<string>();

        using var reader = new StreamReader(inputFile);

        while (true)
        {
            var line = reader.ReadLine();
            
            if (line == null) break;
            
            buffer.Add(line);
            chunkSize += line.Length + Environment.NewLine.Length;

            if (chunkSize < BufferSize) continue;
                
            buffer.Sort();
            WriteChunkToFile(buffer, chunkIndex++);
            buffer.Clear();
            chunkSize = 0;
        }

        if (buffer.Count != 0)
        {
            buffer.Sort();
            WriteChunkToFile(buffer, chunkIndex);
        }

        return chunkSize > 0 ? chunkIndex + 1 : chunkIndex;
    }

    private static void MergeChunks(string outputFile, int chunkCount)
    {
        var readers = new StreamReader[chunkCount];
        var writers = new StreamWriter[chunkCount];
        var lines = new string?[chunkCount];
        var mergedLines = new List<string>();

        for (var i = 0; i < chunkCount; i++)
        {
            readers[i] = new StreamReader($"chunk_{i:D5}.txt");
            writers[i] = new StreamWriter($"chunk_{i:D5}_sorted.txt");
            lines[i] = readers[i].ReadLine(); // чтение первой строки
        }

        while (true)
        {
            string? minLine = null;
            var minIndex = -1;

            for (var i = 0; i < chunkCount; i++)
            {
                if (lines[i] == null)
                    continue;

                if (minLine != null && string.CompareOrdinal(lines[i], minLine) >= 0)
                    continue;
                
                minLine = lines[i];
                minIndex = i;
            }

            if (minLine == null)
                break;

            mergedLines.Add(minLine);

            var nextLine = readers[minIndex].ReadLine();
            lines[minIndex] = nextLine;

            if (nextLine == null)
            {
                readers[minIndex].Close();
                writers[minIndex].Close();
            }
            
            lines[minIndex] = nextLine;
        }

        File.WriteAllLines(outputFile, mergedLines);
    }
    
    private static void WriteChunkToFile(List<string> chunk, int chunkIndex)
    {
        var chunkFileName = $"chunk_{chunkIndex:D5}.txt";
        File.WriteAllLines(chunkFileName, chunk);
    }

    private static void DeleteChunkFiles(int chunkCount)
    {
        for (var i = 0; i < chunkCount; i++)
        {
            var chunkFileName = $"chunk_{i:D5}.txt";
            if (File.Exists(chunkFileName))
                File.Delete(chunkFileName);
            
            var sortedChunkFileName = $"chunk_{i:D5}_sorted.txt";
            if (File.Exists(sortedChunkFileName))
                File.Delete(sortedChunkFileName);
        }
    }
}