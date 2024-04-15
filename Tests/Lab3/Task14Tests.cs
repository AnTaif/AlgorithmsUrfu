using Labs.Lab3;

namespace Tests.Lab3;

public class Task14Tests
{
    
/*
5
Abramovich London 15000000000
Deripaska Moscow 10000000000
Potanin Moscow 5000000000
Berezovsky London 2500000000
Khodorkovsky Chita 1000000000
25 9
1 Abramovich Anadyr
5 Potanin Courchevel
10 Abramovich Moscow
11 Abramovich London
11 Deripaska StPetersburg
15 Potanin Norilsk
20 Berezovsky Tbilisi
21 Potanin StPetersburg
22 Berezovsky London
*/
    [Fact]
    public void SolvingValidInput()
    {
        var billionaires = new Dictionary<string, Person>
        {
            { "Abramovich", new Person("Abramovich", "London", 15000000000) },
            { "Deripaska", new Person("Deripaska", "Moscow", 10000000000) },
            { "Potanin", new Person("Potanin", "Moscow", 5000000000) },
            { "Berezovsky", new Person("Berezovsky", "London", 2500000000) },
            { "Khodorkovsky", new Person("Khodorkovsky", "Chita", 1000000000) },
        };

        var movements = new (int Day, string Name, string City)[]
        {
            (1, "Abramovich", "Anadyr"),
            (5, "Potanin", "Courchevel"),
            (10, "Abramovich", "Moscow"),
            (11, "Abramovich", "London"),
            (11, "Deripaska", "StPetersburg"),
            (15, "Potanin", "Norilsk"),
            (20, "Berezovsky", "Tbilisi"),
            (21, "Potanin", "StPetersburg"),
            (22, "Berezovsky", "London"),
        };

        var actual = Task14.Solve(billionaires, movements, 25);
        
        var expected = new (string City, int Days)[]
        {
            ("Anadyr", 5),
            ("London", 14),
            ("Moscow", 1),
        };
        Assert.Equal(expected, actual);
    }
}