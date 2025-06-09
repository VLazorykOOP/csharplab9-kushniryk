using LR9;
using System.Collections;
using System.Text.RegularExpressions;
using static System.Console;

Console.OutputEncoding = System.Text.Encoding.Unicode;
Console.InputEncoding = System.Text.Encoding.Unicode;

void Task1()
{
    static bool IsOperator(string ch)
    {
        return ch == "+" || ch == "-" || ch == "*" || ch == "/" || ch == "^";
    }
    static string ConvertPostfixToPrefix(string postfix)
    {
        Stack<string> stack = new Stack<string>();
        string[] tokens = postfix.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (string token in tokens)
        {
            if (IsOperator(token))
            {
                string op2 = stack.Pop();
                string op1 = stack.Pop();
                string expr = token + " " + op1 + " " + op2;
                stack.Push(expr);
            }
            else
            {
                stack.Push(token);
            }
        }
        return stack.Pop();
    }
    Console.WriteLine("Введіть постфіксний вираз:");
    string postfix = Console.ReadLine();

    try
    {
        string prefix = ConvertPostfixToPrefix(postfix);
        Console.WriteLine("Префіксний вираз: " + prefix);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка перетворення: " + ex.Message);
    }
}
void Task2()
{
    string filePath = "employees.txt";

    Queue<Employee> lowSalaryQueue = new Queue<Employee>();
    Queue<Employee> highSalaryQueue = new Queue<Employee>();

    try
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 6) continue;

                Employee emp = new Employee
                {
                    LastName = parts[0],
                    FirstName = parts[1],
                    MiddleName = parts[2],
                    Gender = parts[3],
                    Age = int.Parse(parts[4]),
                    Salary = decimal.Parse(parts[5])
                };

                if (emp.Salary < 10000)
                    lowSalaryQueue.Enqueue(emp);
                else
                    highSalaryQueue.Enqueue(emp);
            }
        }

        Console.WriteLine("Співробітники з зарплатою < 10000:");
        foreach (var emp in lowSalaryQueue)
            Console.WriteLine(emp);

        Console.WriteLine("\nІнші співробітники:");
        foreach (var emp in highSalaryQueue)
            Console.WriteLine(emp);
    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка при обробці файлу: " + ex.Message);
    }
}
void Task3()
{
    Console.WriteLine("Введіть постфіксний вираз:");
    string input = Console.ReadLine();

    var converter = new PostfixToPrefixConverter(input);
    Console.WriteLine("Префіксний вираз: " + converter.Convert());

    string filePath = "employees.txt";
    ArrayList allEmployees = new ArrayList();

    // Зчитування з файлу
    foreach (string line in File.ReadLines(filePath))
    {
        if (string.IsNullOrWhiteSpace(line)) continue;
        string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 6) continue;

        allEmployees.Add(new Employee2
        {
            LastName = parts[0],
            FirstName = parts[1],
            MiddleName = parts[2],
            Gender = parts[3],
            Age = int.Parse(parts[4]),
            Salary = decimal.Parse(parts[5])
        });
    }

    Console.WriteLine("\nЗавдання 2:");
    ArrayList lowSalary = new ArrayList();
    ArrayList highSalary = new ArrayList();
    foreach (Employee2 e in allEmployees)
    {
        if (e.Salary < 10000) lowSalary.Add(e.Clone());
        else highSalary.Add(e.Clone());
    }
    PrintList(lowSalary);
    PrintList(highSalary);

    static void PrintList(IEnumerable list)
    {
        foreach (object obj in list)
        {
            Console.WriteLine(obj);
        }
    }
}
void Task4()
{
    MusicCatalog catalog = new MusicCatalog();

    MusicDisk disk1 = new MusicDisk("Jazz Classics");
    MusicDisk disk2 = new MusicDisk("Hip-Hop Legends");
    catalog.AddDisk(disk1);
    catalog.AddDisk(disk2);

    catalog.AddSongToDisk("Jazz Classics", new Song("Take Five", "Dave Brubeck", 324));
    catalog.AddSongToDisk("Jazz Classics", new Song("So What", "Miles Davis", 545));
    catalog.AddSongToDisk("Hip-Hop Legends", new Song("Lose Yourself", "Eminem", 326));
    catalog.AddSongToDisk("Hip-Hop Legends", new Song("Juicy", "The Notorious B.I.G.", 300));

    catalog.DisplayCatalog();

    Console.WriteLine("\nDisplaying Jazz Classics:");
    catalog.DisplayDisk("Jazz Classics");

    Console.WriteLine("\nDisplaying Hip-Hop Legends:");
    catalog.DisplayDisk("Hip-Hop Legends");

    Console.WriteLine("\nSearching for Eminem:");
    catalog.SearchByArtist("Eminem");

    catalog.RemoveSongFromDisk("Hip-Hop Legends", "Juicy");
    Console.WriteLine("\nAfter removing Juicy:");
    catalog.DisplayDisk("Hip-Hop Legends");

    catalog.RemoveDisk("Jazz Classics");
    Console.WriteLine("\nAfter removing Jazz Classics:");
    catalog.DisplayCatalog();
}


Thread thread = new Thread(Task4);
thread.Start();