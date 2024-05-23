namespace Labs.Lab5;

/*
Задача 2. Большая книжка
Ограничение по времени: 5 секунды
Ограничение по памяти: 4 мегабайта
	Заказчику понравилось решение нашей задачи по созданию записной книжки и он предложил нам более сложную задачу:
	создать простую базу данных, которая хранит много записей вида ключ: значение.
	Для работы с книжкой предусмотрены 4 команды:
	ADD KEY VALUE — добавить в базу запись с ключом KEY и значением VALUE. Если такая запись уже есть, вывести ERROR.
	DDELETE KEY — удалить из базы данных запись с ключом KEY. Если такой записи нет — вывести ERROR.
	UPDATE  KEY VALUE — заменить в записи с ключом KEY значение на VALUE. Если такой записи нет — вывести ERROR.
	PRINT KEY — вывести ключ записи и значение через пробел. Если такой записи нет — вывести ERROR.
	Количество входных строк в файле с данными не превышает 300000,  
	количество первоначальных записей равно половине количества строк (первые N/2 команд есть команды ADD).
	Длины ключей и данных не превосходят 4096. 
	Ключи и данные содержат только буквы латинского алфавита и цифры и не содержат пробелов.
	Особенность задачи: все данные не поместятся в оперативной памяти и поэтому придется использовать внешнюю.
*/

public class Task2
{
    private static readonly string rootPath =
        Path.Combine(Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName, "Lab5/task2Database.txt");
    
	public static void Run()
	{
        var n = int.Parse(Console.ReadLine()!);
        var commands = new string[n];
        for (var i = 0; i < n; i++)
            commands[i] = Console.ReadLine()!;

        var log = Solve(commands);
        
        Console.WriteLine("\nOUTPUT:" + (log.Count == 0 ? " empty" : ""));
        foreach (var logItem in log)
            Console.WriteLine(logItem);
    }

    public static List<string> Solve(string[] commands)
    {
        var db = new Database(rootPath);
        
        foreach (var commandLine in commands)
        {
            var command = commandLine.Split();
            switch (command[0])
            {
                case "ADD":
                {
                    db.Add(command[1], command[2]);
                    break;
                }
                case "DELETE":
                {
                    db.Delete(command[1]);
                    break;
                }
                case "UPDATE":
                {
                    db.Update(command[1], command[2]);
                    break;
                }
                case "PRINT":
                {
                    db.Print(command[1]);
                    break;
                }
                default:
                    throw new Exception("Неизвестная команда: " + command[0]);
            }
        }

        return db.GetLog();
    }
}

class Database {
    private readonly HashTable<string, long> index; // Хеш-таблица для индексации ключей и их позиций в файле
    private readonly string dataFilePath;

    private readonly List<string> log = new();
    public List<string> GetLog() => log;

    public Database(string filePath) {
        index = new HashTable<string, long>();
        dataFilePath = filePath;

        File.Create(dataFilePath).Close();
    }

    // Добавление записи
    public void Add(string key, string value) {
        if (index.ContainsKey(key)) {
            Log("ERROR");
            return;
        }

        using var writer = File.AppendText(dataFilePath);
        
        writer.WriteLine($"{key} {value}");
        index.Put(key, writer.BaseStream.Position);
    }

    // Удаление записи
    public void Delete(string key) {
        if (!index.ContainsKey(key)) {
            Log("ERROR");
            return;
        }

        // Удаление из индекса
        index.Remove(key);

        // Удаление из файла
        RewriteDataFile();
    }

    // Обновление записи
    public void Update(string key, string value) {
        if (!index.ContainsKey(key)) {
            Log("ERROR");
            return;
        }

        // Обновление значения в файле
        using (var writer = new StreamWriter(dataFilePath, true)) {
            var position = index.Get(key);
            writer.BaseStream.Seek(position, SeekOrigin.Begin);
            writer.Write($"{key} {value}");
        }
    }

    // Вывод записи
    public void Print(string key) {
        if (!index.ContainsKey(key)) {
            Log("ERROR");
            return;
        }

        // Чтение из файла
        using (var reader = new StreamReader(dataFilePath)) {
            reader.BaseStream.Seek(index.Get(key), SeekOrigin.Begin);
            log.Add(reader.ReadLine()!);
        }
    }

    // Перезапись файла данных без удаленных записей
    private void RewriteDataFile() {
        var tempFilePath = $"{dataFilePath}.temp";
        using (var writer = new StreamWriter(tempFilePath)) {
            using (var reader = new StreamReader(dataFilePath)) {
                string? line;
                while ((line = reader.ReadLine()) != null) {
                    var parts = line.Split(' ');
                    if (index.ContainsKey(parts[0])) {
                        writer.WriteLine(line);
                    }
                }
            }
        }
        // Переименование временного файла
        File.Delete(dataFilePath);
        File.Move(tempFilePath, dataFilePath);
    }

    private void Log(string value)
    {
        log.Add(value);
    }
}