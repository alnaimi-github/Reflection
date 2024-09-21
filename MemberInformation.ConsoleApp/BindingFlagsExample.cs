using System.Reflection;

public sealed class BindingFlagsExample
{
    public void Run()
    {
        var type = typeof(OurExampleType);

        var constructors = type.GetConstructors(
            BindingFlags.Public |
            BindingFlags.NonPublic | 
            BindingFlags.Instance);
        Console.WriteLine($"There are {constructors.Length} constructors.");
        foreach (var constructor in constructors)
        {
            Console.WriteLine($"\tConstructor: {constructor}");
        }

        var events = type.GetEvents(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance);
        Console.WriteLine($"There are {events.Length} events.");
        foreach (var @event in events)
        {
            Console.WriteLine($"\tEvent: {@event}");
        }

        var properties = type.GetProperties(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance);
        Console.WriteLine($"There are {properties.Length} properties.");
        foreach (var property in properties)
        {
            Console.WriteLine($"\tProperty: {property}");
        }

        var methods = type.GetMethods(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance);
        Console.WriteLine($"There are {methods.Length} methods.");
        foreach (var method in methods)
        {
            Console.WriteLine($"\tMethod: {method}");
        }

        var fields = type.GetFields(
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance);
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

        private OurExampleType()
        {
            Console.WriteLine("This is a private constructor!");
        }

        public event EventHandler? SomeEvent;

        public static int StaticProperty { get; set; }

        public string? SomeProperty { get; set; }

        private string? SomePrivateProperty { get; set; }

        public void DoSomething()
        {
        }

        private void DoSomethingElse()
        {
        }
    }
}