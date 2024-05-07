namespace Labs.Lab6;

/*
Задача 3. Танец точек.
Ограничение по времени: 1 секунда
Ограничение по памяти: 256 мегабайт
    На прямой располагается 1 ≤ N ≤ 10000 точек с целочисленными координатами –10^9 ≤ Vi ≤ 10^9. 
    Каждой из точек разрешается сделать ровно одно движение (танцевальное па) в любом направлении на расстояние
     не больше 0 ≤ L ≤ 10^8 и остановиться на другой позиции.
    Какое минимальное количество точек может остаться на прямой после окончания танца
    (все точки после танца, оказывающиеся на одной позиции, сливаются в одну)?
Формат входных данных:
    L N
    V1 V2 … VN
Формат выходных данных:
    MinimalNumberOfPoints
*/

public static class Task3
{
    public static void Run()
    {
        var L = int.Parse(Console.ReadLine()!.Split().First());
        var points = Console.ReadLine()!.Split().Select(int.Parse).ToArray();

        var result = Solve(L, points);
        
        Console.WriteLine(result);
    }

    public static int Solve(int L, int[] points)
    {
        Array.Sort(points); // Сортируем точки по координатам

        var lastPoint = points[0];
        var count = 1;

        for (var i = 1; i < points.Length; i++)
        {
            // Если текущая точка находится на расстоянии 2*L от последней точки, значит она может слиться
            if (lastPoint + 2 * L >= points[i]) continue;
            
            lastPoint = points[i];
            count++;
        }

        return count; // Возвращаем количество оставшихся точек
    }
}