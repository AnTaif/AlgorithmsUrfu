namespace Labs.Lab1;

/*
Задача 7. Магараджа.
    Ограничение по времени: 1 секунда
    Ограничение по памяти: 16 мегабайт
    Магараджа — это шахматная фигура, сочетающая возможности ферзя и коня. Таким
    образом, магараджа может ходить и бить на любое количество клеток по диагонали,
    горизонтали и вертикали (т.е. как ферзь), а также либо на две клетки по горизонтали и на
    одну по вертикали, либо на одну по горизонтали и на две по вертикали (как конь).
    Ваша задача — найти число способов расставить на доске N на N ровно K
    магараджей так, чтобы они не били друг друга.
Формат входных данных:
    Входной файл INPUT.TXT содержит два целых числа: N и K (1 ≤ K ≤ N ≤ 10).
Формат выходных данных:
    В выходной файл OUTPUT.TXT выведите ответ на задачу
*/

public static class Task7
{
    static int N;
    
    static bool[,] board = null!;
    static bool[] diagR = null!; // диагональ '/' (row + col)
    static bool[] diagL = null!; // диагональ '\' (row - col + N - 1)
    
    private static string rootPath = Directory.GetParent(Environment.CurrentDirectory)!.Parent!.Parent!.FullName; 

    public static void Run()
    {
        using var reader = new StreamReader(Path.Combine(rootPath, "Lab1/input7.txt"));
        using var writer = new StreamWriter(Path.Combine(rootPath, "Lab1/output7.txt"));
        
        var input = reader.ReadLine()!.Split(' ');
        var n = int.Parse(input[0]);
        var k = int.Parse(input[1]);

        var count = CountWays(n, k);

        writer.Write(count);
    }

    public static int CountWays(int n, int k)
    {
        N = n;
        
        board = new bool[N, N];
        
        diagR = new bool[N * 2];
        diagL = new bool[N * 2];
        
        return CountWaysRecursive(0, 0, k);
    }

    private static int CountWaysRecursive(int row, int col, int k)
    {
        if (k == 0)
            return 1;

        var count = 0;

        for (var i = row; i < N; i++)
        {
            for (var j = (i == row ? col : 0); j < N; j++)
            {
                if (!IsSafe(i, j))
                    continue;

                // Ставим магараджу
                SetFigure(i, j, true);
                // Находим количество расстановок следующих магарадж
                count += CountWaysRecursive(i, j + 1, k - 1);
                // Убираем магараджу
                SetFigure(i, j, false);
            }
        }

        return count;
    }

    private static void SetFigure(int row, int col, bool figure)
    {
        diagR[row + col] = diagL[row - col + N - 1] = figure;
        board[row, col] = figure;
    }

    private static bool IsSafe(int row, int col)
    {
        if (board[row, col])
            return false;

        // Проверка горизонтали / вертикали
        for (var i = 0; i < N; i++)
            if (board[row, i] || board[i, col])
                return false;

        // Проверка диагоналей
        if (diagR[row + col] || diagL[row - col + N - 1])
            return false;

        return CheckKnightMove(row, col);
    }

    // Проверка хода коня
    private static bool CheckKnightMove(int row, int col)
    {
        var dx = new[] { -2, -1, 1, 2, -2, -1, 1, 2 };
        var dy = new[] { -1, -2, -2, -1, 1, 2, 2, 1 };
        for (var i = 0; i < dx.Length; i++)
        {
            var x = row + dx[i];
            var y = col + dy[i];
            if (x >= 0 && x < N && y >= 0 && y < N && board[x, y])
            {
                return false;
            }
        }
        return true;
    }
}