namespace Labs.Lab6;

/*
Задача 2. Маршрутка
Ограничение по времени: 1 секунда
Ограничение по памяти: 64 мегабайта
    Вступление
    Водитель маршрутки Сергей Александрович Жадных прославился своей
    феноменальной жадностью повсеместно. Он сам неоднократно заявлял, что за лишнюю
    копейку готов задушить родного брата и продать всех друзей. К сожалению, проверить эти
    слова не представлялось возможным, поскольку никакого брата, а также друзей, дома и
    семьи у Сергея Александровича не было. И что ещё хуже, денег у него тоже не было.
    Единственным достоянием г-на Жадных являлась старая маршрутка, на которой он и
    колесил по городу, подвозя редких пассажиров и время от времени осматривая краем глаза
    тротуары в поисках мелких монет...
    В один из дней небеса сжалились на Сергеем Александровичем и решили прекратить
    его бесполезное существование в этом жестоком мире. С сей благой целью на голову ничего
    не подозревавшего г-на Жадных, вышедшего из маршрутки на призывный блеск 5-
    рублёвой монеты, свалился топор. Мечты о выгодной продаже бутылки мигом вылетели у
    него из головы, поскольку их место занял топор. В переносном смысле этого слова. Орудие
    небес не смогло пробить окостеневший череп г-на Жадных, однако, как выяснилось
    позднее, придало ему несколько весьма полезных свойств.
    Вскоре после продажи топора Сергей Александрович обнаружил, что способен
    предвидеть будущее. Какие возможности, какие перспективы открылись незадачливому
    водителю маршрутки! Кто мы такие и куда идём? Чего бояться и на что надеяться? Ответы
    на эти вопросы г-на Жадных совершенно не интересовали. А вот на то, чтобы с помощью
    новых умений попытаться заработать немного денег, выполняя привычную работу,
    сообразительности у Сергея Александровича хватило.
    Задача
    Ежедневно маршрутка совершает один рейс от первой до N-й остановки. В
    маршрутке M мест для пассажиров. Вечером, просчитав линии вероятностей, г-н Жадных
    (между прочим, потенциальный Тёмный Иной шестого уровня) выяснил, что завтра на
    остановках маршрутку будут поджидать K человек. Для каждого человека были
    определены номер остановки S[i], на которой он желает сесть в маршрутку, и номер
    остановки F[i], на которой он собирается выйти. В соответствии с ценовой политикой
    Сергея Александровича, каждый пассажир должен заплатить P рублей за билет независимо
    от количества остановок. Более того, притормозив на остановке, г-н Жадных может
    выбирать, кого из желающих посадить в маршрутку, а кого нет. Ставя перед собой задачу
    максимизации прибыли, Сергей Александрович вполне разумно решил определить, каких
    именно людей нужно сажать в маршрутку. К сожалению, для этого его сил оказалось
    недостаточно. А Ваших?
Формат входных данных:
    Первая строка содержит целые числа N (2 ≤ N ≤ 100000), M (1 ≤ M ≤ 1000), K (0 ≤ K
    ≤ 50000) и P (1 ≤ P ≤ 10000). Каждая из следующих K строк содержит целые числа S[i] и
    F[i] (1 ≤ S[i] < F[i] ≤ N) для соответствующего человека.
Формат выходных данных:
    В первую строку вывести максимальную прибыль. Во вторую строку вывести через
    пробел и в любом порядке номера людей, которых следует сажать в маршрутку для
    получения этой прибыли. Если задача имеет несколько решений, то вывести любое из них.
*/

public static class Task2
{
    public static void Run()
    {
        var input = Console.ReadLine()!.Split();
        var N = int.Parse(input[0]);
        var M = int.Parse(input[1]);
        var K = int.Parse(input[2]);
        var P = int.Parse(input[3]);

        var passengers = new (int Start, int End)[K];
        for (var i = 0; i < K; i++)
        {
            var passengerInput = Console.ReadLine()!.Split();
            var start = int.Parse(passengerInput[0]);
            var end = int.Parse(passengerInput[1]);
            passengers[i] = (start, end);
        }

        var result = Solve(N, M, P, passengers);
        Console.WriteLine(result.Income);
        Console.WriteLine(string.Join(" ", result.Passengers));
    }

    public static (int Income, int[] Passengers) Solve(int N, int M, int P, (int Start, int End)[] passengers)
    {
        Array.Sort(passengers, (x, y) => x.Start.CompareTo(y.Start)); // Сортируем пассажиров по стартовой остановке

        var income = 0;
        var passengersList = new List<int>();

        var freeSeats = M;
        var currentStop = 1;

        foreach (var (start, end) in passengers)
        {
            if (start > currentStop)
            {
                var availableSeats = Math.Min(freeSeats, start - currentStop); // Максимальное количество пассажиров, которых можно взять до следующей остановки
                for (int i = 0; i < availableSeats; i++)
                {
                    income += P;
                    passengersList.Add(currentStop + i);
                }
                freeSeats -= availableSeats;
                currentStop = start;
            }

            if (freeSeats == 0)
                break;

            income += P;
            passengersList.Add(start);
            freeSeats--;
        }

        return (income, passengersList.ToArray());
    }
}