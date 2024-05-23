namespace Labs.Lab4;

/*
Задача 3. Телефонная книга
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайта
    Необходимо разработать программу, которая является промежуточным звеном в
    реализации телефонной книги. На вход подается N ≤ 1000 команд вида
    ADD User Number
    DELETE User
    EDITPHONE User Number
    PRINT User
    Согласно этим командам нужно соответственно добавить пользователя в
    телефонную книгу, удалить пользователя, изменить его номер и вывести на экран его
    данные. В случае невозможности выполнить действие, необходимо вывести ERROR.
    Добавлять пользователя, уже существующего в телефонной книге, нельзя.
    Необходимо вывести протокол работы телефонной книги
Формат входных данных:
    Команды: ADD User Number, DELETE User, EDITPHONE User Number, PRINT User
Формат выходных данных:
    В случае невозможности выполнения действия требуется вывести ERROR
    В случае команды PRINT User
    User Number 
*/

public static class Task3
{
    public static void Run()
    {
        var n = int.Parse(Console.ReadLine()!);
        var commands = new string[n];

        for (var i = 0; i < n; i++)
            commands[i] = Console.ReadLine()!;
        
        var log = Solve(commands);
        
        foreach (var logValue in log)
            Console.WriteLine(logValue);
    }

    public static List<string> Solve(string[] commands)
    {
        var phoneBook = new PhoneBook();
        
        foreach (var commandLine in commands)
        {
            var command = commandLine.Split();
            
            var action = command[0];
            var name = command[1];
            var phoneNumber = command.Length > 2 ? command[2] : null;

            switch (action)
            {
                case "ADD":
                {
                    phoneBook.Add(name, phoneNumber!);
                    break;
                }
                case "DELETE":
                {
                    phoneBook.Delete(name);
                    break;
                }
                case "EDITPHONE":
                {
                    phoneBook.Edit(name, phoneNumber!);
                    break;
                }
                case "PRINT":
                {
                    phoneBook.Print(name);
                    break;
                }
                default:
                    throw new ArgumentException("Invalid command");
            }
        }

        return phoneBook.GetLog();
    }
}

// Бинарное дерево
class PhoneBook
{
    private PhoneBookNode? root;

    private readonly List<string> log = new();
    public List<string> GetLog() => log;
    
    public void Add(string user, string number) => root = Insert(root, user, number);

    private PhoneBookNode Insert(PhoneBookNode? node, string user, string number)
    {
        if (node == null)
        {
            node = new PhoneBookNode(user, number);
            return node;
        }

        var compare = string.CompareOrdinal(user, node.User);
        switch (compare)
        {
            case < 0:
                node.Left = Insert(node.Left, user, number);
                break;
            case > 0:
                node.Right = Insert(node.Right, user, number);
                break;
            default:
                log.Add("ERROR");
                break;
        }

        return node;
    }

    public void Delete(string user) => root = Delete(root, user);

    private PhoneBookNode? Delete(PhoneBookNode? node, string user)
    {
        if (node == null)
        {
            log.Add("ERROR");
            return node;
        }

        var compare = string.CompareOrdinal(user, node.User);
        switch (compare)
        {
            case < 0:
                node.Left = Delete(node.Left, user);
                break;
            case > 0:
                node.Right = Delete(node.Right, user);
                break;
            default:
            {
                if (node.Left == null)
                    return node.Right;
                if (node.Right == null)
                    return node.Left;

                node.User = FindMin(node.Right).User;
                node.Right = Delete(node.Right, node.User);
                break;
            }
        }

        return node;
    }

    private PhoneBookNode FindMin(PhoneBookNode node)
    {
        while (node.Left != null)
            node = node.Left;
        
        return node;
    }

    public void Edit(string user, string newNumber) => root = Edit(root, user, newNumber);

    private PhoneBookNode? Edit(PhoneBookNode? node, string user, string newNumber)
    {
        if (node == null)
        {
            log.Add("ERROR");
            return null;
        }

        var compare = string.CompareOrdinal(user, node.User);
        switch (compare)
        {
            case < 0:
                node.Left = Edit(node.Left, user, newNumber);
                break;
            case > 0:
                node.Right = Edit(node.Right, user, newNumber);
                break;
            default:
                node.Number = newNumber;
                break;
        }

        return node;
    }

    public void Print(string user)
    {
        var node = Find(root, user);
        log.Add(node != null ? $"{node.User} {node.Number}" : "ERROR");
    }

    private PhoneBookNode? Find(PhoneBookNode? node, string user)
    {
        if (node == null)
            return null;

        var compare = string.CompareOrdinal(user, node.User);
        return compare switch
        {
            < 0 => Find(node.Left, user),
            > 0 => Find(node.Right, user),
            _ => node
        };
    }
}

class PhoneBookNode(string user, string number)
{
    public string User { get; set; } = user;
    public string Number { get; set; } = number;
    public PhoneBookNode? Left { get; set; }
    public PhoneBookNode? Right { get; set; }
}