
public sealed class GetMembersOfTypeExample
{
    public void Run()
    {
        var type = typeof(OurExampleType);

        var constructors = type.GetConstructors();
        Console.WriteLine($"There are {constructors.Length} constructors.");
        foreach (var constructor in constructors)
        {
            Console.WriteLine($"\tConstructor: {constructor}");
        }

        var events = type.GetEvents();
        Console.WriteLine($"There are {events.Length} events.");
        foreach (var @event in events)
        {
            Console.WriteLine($"\tEvent: {@event}");
        }

        var properties = type.GetProperties();
        Console.WriteLine($"There are {properties.Length} properties.");
        foreach (var property in properties)
        {
            Console.WriteLine($"\tProperty: {property}");
        }

        var methods = type.GetMethods();
        Console.WriteLine($"There are {methods.Length} methods.");
        foreach (var method in methods)
        {
            Console.WriteLine($"\tMethod: {method}");
        }

        // how do we see the private fields though?!
        var fields = type.GetFields();
        Console.WriteLine($"There are {fields.Length} fields.");
        foreach (var field in fields)
        {
            Console.WriteLine($"\tField: {field}");
        }
    }

    public sealed class OurExampleType
    {
        private readonly int _someField;

        public OurExampleType(int someField)
        {
            _someField = someField;
        }

        public event EventHandler? SomeEvent;

        public string? SomeProperty { get; set; }

        public void DoSomething()
        {
        }
    }
}