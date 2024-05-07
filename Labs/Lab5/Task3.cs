namespace Labs.Lab5;

/*
Задача 3. Сопоставление по образцу
Ограничение по времени: 2 секунды
Ограничение по памяти: 64 мегабайта
	Известно, что при работе с файлами можно указывать метасимволы * и ? для отбора нужной группы файлов,
	причем знак * соответствует любому множеству, даже пустому, в имени файла, 
	а символ ? Соответствует ровно одному символу в имени.
	Первая строка программы содержит имя файла, состоящее только из заглавных букв латинского языка (A-Z), 
	а вторая — образец, содержащий только заглавные буквы латинского алфавита  и, возможно, символы * и ?. 
	Строки не превышают по длине 700 символов. 
	Требуется вывести слова YES или NO в зависимости от того, сопоставляется ли имя файла указанному образцу.
Формат входных данных	
	SOMETEXT
	PATTERN
Формат выходных данных
	YES
	или
	NO
*/

public class Task3
{
    public static void Run()
    {
	    var fileName = Console.ReadLine()!;
	    var pattern = Console.ReadLine()!;

	    var isMatch = Solve(fileName, pattern);

	    Console.WriteLine(isMatch ? "YES" : "NO");
    }

    public static bool Solve(string fileName, string pattern)
    {
	    var nameIndex = 0;
	    var patternIndex = 0;
	    
	    var starIndex = -1; // Позиция '*' в образце
	    var lastMatchIndex = -1; // Позиция последнего совпадения после '*'

	    while (nameIndex < fileName.Length) {
		    // Если символы совпадают или в образце '?', или дошли до конца образца '*'
		    if (patternIndex < pattern.Length && (fileName[nameIndex] == pattern[patternIndex] || pattern[patternIndex] == '?')) {
			    nameIndex++;
			    patternIndex++;
		    }
		    // Если встретился '*', запоминаем его позицию и позицию последнего совпадения
		    else if (patternIndex < pattern.Length && pattern[patternIndex] == '*') {
			    starIndex = patternIndex;
			    lastMatchIndex = nameIndex;
			    patternIndex++;
		    }
		    // Если дошли до '*' в образце, но символы не совпадают, двигаемся по имени файла
		    else if (starIndex != -1) {
			    patternIndex = starIndex + 1;
			    lastMatchIndex++;
			    nameIndex = lastMatchIndex;
		    }
		    // Если символы не совпадают и нет '*', возвращаем false
		    else
			    return false;
	    }

	    // Пропускаем оставшиеся '*' в образце
	    while (patternIndex < pattern.Length && pattern[patternIndex] == '*')
		    patternIndex++;

	    // Если дошли до конца обоих массивов символов, значит сопоставление успешно
	    return patternIndex == pattern.Length;
    }
}