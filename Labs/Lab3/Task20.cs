namespace Labs.Lab3;

/*
Задача 20. Ксерокопии
Ограничение по времени: 0,2 секунды
Ограничение по памяти: 16 мегабайт
    Секретарша Людоча сегодня опоздала на работу и ей срочно нужно успеть к обеду
    сделать N копий одного документа. В ее распоряжении имеются два ксерокса, один из
    которых копирует лист за х секунд, а другой – за y секунд. (Разрешается использовать как
    один ксерокс, так и оба одновременно. Можно копировать не только с оригинала, но и с
    копии.) Помогите ей выяснить, какое минимальное время для этого потребуется.
Формат входных данных:
    Во входном файле INPUT.TXT записаны три натуральных числа N, x и y,
    разделенные пробелом (1 ≤ N ≤ 2∙108, 1 ≤ x, y ≤ 10).
Формат выходных данных:
    В выходной файл OUTPUT.TXT выведите одно число – минимальное время в
    секундах, необходимое для получения N копий.
    
4 1 1 => 3
5 1 2 => 4
*/

public static class Task20
{
    public static void Run()
    {
        // Чтение данных из файла
        var input = Console.ReadLine()!.Split();
        var N = int.Parse(input[0]); // Количество копий
        var x = int.Parse(input[1]); // Время копирования первым ксероксом
        var y = int.Parse(input[2]); // Время копирования вторым ксероксом

        if (x > y)
            (x, y) = (y, x);

        var lcm = LCM(x, y); // наибольшее общее кратное
        var parallelCopyCount = lcm / x + lcm / y; // кол-во скопированных страниц при параллельной работе ксероксов

        var copiesCount = N-1;
        var minTime = x;
        
        minTime += copiesCount / parallelCopyCount * lcm; // добавляем время когда оба ксерокса работали
        copiesCount -= parallelCopyCount;

        var timeX = 0;
        var timeY = 0;
        while (copiesCount > 0)
        {
            minTime++;
            timeX++;
            timeY++;
            if (timeX == x)
            {
                copiesCount--;
                timeX = 0;
            }
            if (timeY == y)
            {
                copiesCount--;
                timeY = 0;
            }
        }

        Console.WriteLine(minTime);
    }

    // Наибольшее общее кратное (НОК)
    private static int LCM(int x, int y) => x * y / GCD(x, y);

    // Наибольший общий делитель (НОД)
    private static int GCD(int x, int y)
    {
        while (y != 0)
        {
            var temp = y;
            y = x % y;
            x = temp;
        }
        return x;
    }
}