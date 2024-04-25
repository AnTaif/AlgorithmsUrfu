namespace Labs.Lab3;

/*
Задача 14. Миллиардеры
Ограничение времени: 3 секунды.
Ограничение памяти: 64 МБ
    Возможно, вы знаете, что из всех городов мира больше всего миллиардеров живёт в
    Москве. Но, поскольку работа миллиардера подразумевает частые перемещения по всему
    свету, в определённые дни какой-то другой город может занимать первую строчку в таком
    рейтинге. Ваши приятели из ФСБ, ФБР, MI5 и Шин Бет скинули вам списки перемещений
    всех миллиардеров за последнее время. Ваш работодатель просит посчитать, сколько дней
    в течение этого периода каждый из городов мира был первым по общей сумме денег
    миллиардеров, находящихся в нём.
Формат входных данных:
    В первой строке записано число n — количество миллиардеров (1 ≤ n ≤ 10000).
    Каждая из следующих n строк содержит данные на определённого человека: его имя,
    название города, где он находился в первый день данного периода, и размер состояния. В
    следующей строке записаны два числа: m — количество дней, о которых есть данные (1
    ≤ m ≤ 50000), k — количество зарегистрированных перемещений миллиардеров (0 ≤ k ≤
    50000). Следующие k строк содержат список перемещений в формате: номер дня (от 1
    до m − 1), имя человека, название города назначения. Вы можете считать, что миллиардеры
    путешествуют не чаще одного раза в день, и что они отбывают поздно вечером и прибывают
    в город назначения рано утром следующего дня. Список упорядочен по возрастанию
    номера дня. Все имена и названия городов состоят не более чем из 20 латинских букв,
    регистр букв имеет значение. Состояния миллиардеров лежат в пределах от 1 до 100
    миллиардов.
Формат выходных данных:
    В каждой строке должно содержаться название города и, через пробел, количество
    дней, в течение которых этот город лидировал по общему состоянию миллиардеров,
    находящихся в нём. Если таких дней не было, пропустите этот город. Города должны быть
    отсортированы по алфавиту (используйте обычный порядок символов: ABC...Zabc...z).
*/

public static class Task14
{
    public static void Run()
    {
        var personDictionary = new Dictionary<string, Person>();
        
        var n = int.Parse(Console.ReadLine()!);

        for (var i = 0; i < n; i++)
        {
            var inputPerson = Console.ReadLine()!.Split();
            var person = new Person(inputPerson[0], inputPerson[1], long.Parse(inputPerson[2]));
            personDictionary[person.Name] = person;
        }

        var inputTemp = Console.ReadLine()!.Split();
        var m = int.Parse(inputTemp[0]);
        var k = int.Parse(inputTemp[1]);

        var movements = new (int Day, string Name, string City)[k];

        for (var i = 0; i < k; i++)
        {
            var inputMovement = Console.ReadLine()!.Split();
            movements[i] = (int.Parse(inputMovement[0]), inputMovement[1], inputMovement[2]);
        }

        var cityTop = Solve(personDictionary, movements, m);

        foreach (var (city, daysInTop) in cityTop)
            Console.WriteLine($"{city} {daysInTop}");
    }

    public static (string City, int DaysInTop)[] Solve(
        Dictionary<string, Person> personDictionary, 
        (int Day, string Name, string City)[] movements, 
        int maxDay)
    {
        var cityMoney = new Dictionary<string, long>();
        var moneyCities = new SortedDictionary<long, HashSet<string>>();
        var cityTop = new Dictionary<string, int>();

        foreach (var person in personDictionary.Values)
            UpdatePersonData(cityMoney, moneyCities, person);            
        
        var previousDay = 0;
        int currentDay;
        
        foreach (var (day, name, city) in movements)
        {
            currentDay = day;
            UpdateCityTop(moneyCities, cityTop, previousDay, currentDay);

            var oldCity = personDictionary[name].City;
            RemoveMoneyCity(moneyCities, oldCity, cityMoney[oldCity]);
            cityMoney[oldCity] -= personDictionary[name].Money;
            AddMoneyCity(moneyCities, cityMoney, oldCity, cityMoney[oldCity]);
            
            cityMoney.TryAdd(city, 0);
            RemoveMoneyCity(moneyCities, city, cityMoney[city]);
            cityMoney[city] += personDictionary[name].Money;
            AddMoneyCity(moneyCities, cityMoney, city, cityMoney[city]);
        
            personDictionary[name].City = city;
            
            previousDay = currentDay;
        }
        
        currentDay = maxDay;
        UpdateCityTop(moneyCities, cityTop, previousDay, currentDay);

        var result = new List<(string City, int DaysInTop)>();
        foreach (var (city, top) in cityTop)
            result.Add((city, top));
        
        result.Sort();
        return result.ToArray();
    }

    private static void RemoveMoneyCity(SortedDictionary<long, HashSet<string>> moneyCities, string city, long money)
    {
        if (!moneyCities.TryGetValue(money, out var cities)) return;
        
        cities.Remove(city);
        if (cities.Count == 0)
            moneyCities.Remove(money);
    }

    private static void AddMoneyCity(
        SortedDictionary<long, HashSet<string>> moneyCities,
        Dictionary<string, long> cityMoney,
        string city, long money)
    {
        if (!moneyCities.ContainsKey(cityMoney[city]))
            moneyCities[money] = new HashSet<string>();

        moneyCities[cityMoney[city]].Add(city);

    }

    private static void UpdatePersonData(
        Dictionary<string, long> cityMoney, 
        SortedDictionary<long, HashSet<string>> moneyCities, 
        Person person)
    {
        var city = person.City;
        var money = person.Money;

        if (cityMoney.TryGetValue(person.City, out var sum))
        {
            moneyCities[sum].Remove(city);
            if (moneyCities[sum].Count == 0)
                moneyCities.Remove(sum);
        }
        else
            cityMoney[city] = 0;

        cityMoney[city] += money;

        if (!moneyCities.ContainsKey(cityMoney[city]))
            moneyCities[cityMoney[person.City]] = new HashSet<string>();

        moneyCities[cityMoney[city]].Add(city);
    }

    private static void UpdateCityTop(
        SortedDictionary<long, HashSet<string>> moneyCities, 
        Dictionary<string, int> cityTop, 
        int previousDay, int currentDay)
    {
        var topCities = moneyCities.Last().Value;
        if (currentDay == previousDay || topCities.Count != 1) return;
        
        var topCityName = topCities.Single();
        cityTop[topCityName] = cityTop.GetValueOrDefault(topCityName) + (currentDay - previousDay);
    }
}

public class Person(string name, string city, long money)
{
    public string Name { get; } = name;
    public string City { get; set; } = city;
    public long Money { get; } = money;
}
