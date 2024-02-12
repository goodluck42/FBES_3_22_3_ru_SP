using System.Text.Json;

namespace TPL;

class Person
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}


interface IPersonManager
{
    void AddPerson(Person person);
    Task AddPersonAsync(Person person);
    Person GetPersonById(int id);
    Task<Person> GetPersonByIdAsync(int id);
    IEnumerable<Person> GetAllPersons();
    Task<IEnumerable<Person>> GetAllPersonsAsync();
}

class LocalPersonManager : IPersonManager
{
    private List<Person> _list;

    public LocalPersonManager()
    {
        _list = new();
    }
    
    public void AddPerson(Person person)
    {
        _list.Add(person);
    }

    public Task AddPersonAsync(Person person)
    {
        Console.WriteLine($"AddPersonAsync: {Thread.CurrentThread.ManagedThreadId}");

        _list.Add(person);

        return Task.CompletedTask;
    }

    public Person GetPersonById(int id)
    {
        return _list.First(p => p.Id == id);
    }

    public Task<Person> GetPersonByIdAsync(int id)
    {
        return Task.FromResult(GetPersonById(id));
    }

    public IEnumerable<Person> GetAllPersons()
    {
        return _list;
    }

    public Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        return Task.FromResult(GetAllPersons());
    }
}

class JsonPersonManager : IPersonManager
{
    private readonly string _filename;
    
    public JsonPersonManager(string filename)
    {
        _filename = filename;

        if (!File.Exists(_filename))
        {
            using var streamWriter = new StreamWriter(_filename);
            
            streamWriter.Write("[]");
        }
    }
    
    public void AddPerson(Person person)
    {
        var persons = ReadData().ToList();
        
        persons.Add(person);
        
        SaveData(persons);
    }

    public Task AddPersonAsync(Person person)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"AddPersonAsync: {Thread.CurrentThread.ManagedThreadId}");
            
            AddPerson(person);
        });
    }

    public Person GetPersonById(int id)
    {
        return ReadData().First(p => p.Id == id);
    }

    public Task<Person> GetPersonByIdAsync(int id)
    {
        return Task.Run(() =>
        {
            Console.WriteLine($"GetPersonByIdAsync: {Thread.CurrentThread.ManagedThreadId}");
            
            return GetPersonById(id);
        });
    }

    public IEnumerable<Person> GetAllPersons()
    {
        return ReadData();
    }

    public Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        return Task.Run(ReadData);
    }

    private IEnumerable<Person> ReadData()
    {
        return JsonSerializer.Deserialize<List<Person>>(File.ReadAllText(_filename)) ?? Enumerable.Empty<Person>();
    }
    
    private void SaveData(IEnumerable<Person> data)
    {
        File.WriteAllText(_filename, JsonSerializer.Serialize(data));
    }
}

class Program
{
    static async Task Main(string[] args)
    {
        IPersonManager manager = new JsonPersonManager("persons.json");
        
        var addTask = manager.AddPersonAsync(new Person()
        {
            Id = 1,
            FirstName = "Vladik",
            LastName = "Petrov"
        });

        var getTask = manager.GetPersonByIdAsync(1);
        
        Console.WriteLine($"main: {Thread.CurrentThread.ManagedThreadId}");

        await addTask;
        var person = await getTask;

        Console.WriteLine(person.Id);
        Console.WriteLine(person.FirstName);
        Console.WriteLine(person.LastName);

        // var result = Task.Run(() =>
        // {
        //     Console.WriteLine($"Thread id in task {Thread.CurrentThread.ManagedThreadId}");
        //     int s = 0;
        //     for (int i = 0; i < 5; i++)
        //     {
        //         s += i;
        //         Console.WriteLine(i);
        //     }
        //
        //     return s;
        // });
        //
        // Console.WriteLine($"Thread id in main {Thread.CurrentThread.ManagedThreadId}");
        //
        // Thread.Sleep(1000);
        //
        // Console.WriteLine(await result);
    }
}