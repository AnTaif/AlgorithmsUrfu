namespace Labs.Lab8;

/*
Задача 1. Зелье
Ограничение по времени: 1 секунда
Ограничение по памяти: 256 мегабайт
    Злой маг Крокобобр варит зелье. У него есть большая колба, которую можно ставить
    на огонь и две колбы поменьше, которые огня не выдержат. В большой колбе налита
    компонента зелья, которую нужно подогреть на огне, маленькие колбы пусты. Емкость
    большой колбы магу Крокобобру известна — N миллилитров, маленьких — M и K
    миллитров. Смесь можно переливать из любой колбы в любую, если выполняется одно из
    условий: либо после переливания одна из колб становится пустой, либо одна из колб
    становится полной, частичные переливания недопустимы.
    Требуется ровно L миллилитров смеси в большой колбе. Помогите Крокобобру
    определить, сколько переливаний он должен сделать для этого.
Формат входных данных
    N M K L, 1 ≤ N, M, K, L ≤ 2000
Формат выходных данных
    Одно число, равное количеству необходимых переливаний или слово OOPS, если
    это невозможно.
*/

/*
IN: 10 6 5 8
OUT: 7
*/

public static class Task1
{
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var N = int.Parse(input[0]);
        var M = int.Parse(input[1]);
        var K = int.Parse(input[2]);
        var L = int.Parse(input[3]);

        Console.WriteLine(Solve(N, M, K, L));
    }

    public static string Solve(int N, int M, int K, int L)
    {
        if (L > N) return "OOPS";
        
        var visited = new HashSet<(int, int, int)>();
        var queue = new Queue<((int, int, int) state, int count)>();
        
        // начальное состояние (В большой колбе N мл есть жидкости, маленькие пусты)
        queue.Enqueue(((0, 0, N), 0));
        visited.Add((0, 0, N));
        
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            var state = current.state;
            var count = current.count;

            var big = state.Item3;

            if (big == L) return count.ToString();

            var nextStates = GetNextStates(state, N, M, K);

            foreach (var nextState in nextStates)
            {
                if (!visited.Contains(nextState))
                    queue.Enqueue((nextState, count + 1));
                
                visited.Add(nextState);
            }
        }

        return "OOPS";
    }

    private static List<(int, int, int)> GetNextStates((int, int, int) state, int N, int M, int K)
    {
        var (small1, small2, big) = state;
        var nextStates = new List<(int, int, int)>();

        if (big > 0)
        {
            // Переливание из большой колбы в маленькую первую (растягивает до полной или до опустошения большой)
            if (small1 < M)
            {
                var pour = Math.Min(big, M - small1);
                nextStates.Add((small1 + pour, small2, big - pour));
            }
            // Переливание в маленькую вторую
            if (small2 < K)
            {
                var pour = Math.Min(big, K - small2);
                nextStates.Add((small1, small2 + pour, big - pour));
            }
        }

        if (small1 > 0)
        {
            // Переливание из маленькой первой в большую
            if (big < N)
            {
                var pour = Math.Min(N - big, small1);
                nextStates.Add((small1 - pour, small2, big + pour));
            }
            // Переливание в маленькую вторую
            if (small2 < K)
            {
                var pour = Math.Min(K - small2, small1);
                nextStates.Add((small1 - pour, small2 + pour, big));
            }
        }

        if (small2 > 0)
        {
            // Переливание из маленькой второй в большую
            if (big < N)
            {
                var pour = Math.Min(N - big, small2);
                nextStates.Add((small1, small2 - pour, big + pour));
            }
            // Переливание в маленькую первую
            if (small1 < M)
            {
                var pour = Math.Min(M - small1, small2);
                nextStates.Add((small1 + pour, small2 - pour, big));
            }
        }

        return nextStates;
    }
}