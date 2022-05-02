const string FILE_NAME = "AnimalFarm.txt";
char[] SPLIT = { ',','.', '-', ' '};
HashSet<string> IGNORE = new HashSet<string>(new[] { "and", "the", "when"});

bool exists = File.Exists(FILE_NAME);

Console.WriteLine($"exist = {exists}");

//var txt = File.ReadAllText(FILE_NAME);
//Console.WriteLine(txt.Substring(0, 100));


var lines = File.ReadLines(FILE_NAME);
//var notEmpty = lines.Where(x => !string.IsNullOrWhiteSpace(x));
var notEmpty = from x in lines
               where !string.IsNullOrWhiteSpace(x)
               select x;

//var words = notEmpty.SelectMany(x => x.Split(SPLIT, StringSplitOptions.RemoveEmptyEntries));
var words = from x in notEmpty
            from x1 in x.Split(SPLIT, StringSplitOptions.RemoveEmptyEntries)
            select x1;

var clean = words.Select(x => x.ToLower())
                 .Where(x => x.Length > 2 && !IGNORE.Contains(x));
//var groups = clean.GroupBy(x => x, x => null as object);
var groups = from x in clean
             group null as object by x;

//var counts = groups.Select(x => new { Word = x.Key, Count=x.Count() });
//var counts = groups.Select(x => ( Word: x.Key, Count:x.Count() ));
var counts = groups.Select(x => new WordCount( Word: x.Key, Count:x.Count() ));
var sort = counts.OrderByDescending(x => x.Count);
var top10 = sort.Take(10);

//foreach (var item in top10)
//{
//    Console.WriteLine($"{item.Word}: {item.Amount}");
//}

//foreach ((string Word, int Count) item in top10)
//{
//    Console.WriteLine($"{item.Word}: {item.Count}");
//}

//foreach ((string Word, int Amount) item in top10)
//{
//    Console.WriteLine($"{item.Word}: {item.Amount}");
//}

foreach (WordCount item in top10)
{
    Console.WriteLine($"{item.Word}: {item.Count}");
}

Console.ReadKey();


record WordCount(string Word, int Count);
