using System.Reflection;

public sealed class ConstructorInfoExample
{
    public void RunExample()
    {
        var type = typeof(OurClass);

        var bindingFlags =
            BindingFlags.Public |
            BindingFlags.NonPublic |
            BindingFlags.Instance;
        var constructors = type.GetConstructors(bindingFlags);

        var parameterlessConstructor = constructors
            .FirstOrDefault(x => x
            .GetParameters().Length == 0);
        var instance = parameterlessConstructor?.Invoke(null);
        Console.WriteLine();

        var parameterizedConstructor = constructors
            .FirstOrDefault(x => x
            .GetParameters().Length == 2);
        var instance2 = parameterizedConstructor?.Invoke(
            [112233,
            "Dev Leader Rocks!"]);
        Console.WriteLine();

        var privateConstructor = constructors
            .FirstOrDefault(x => x.IsPrivate);
        var instance3 = privateConstructor?.Invoke(
            [98765,
            "Reflection is super cool!",
            Math.PI]);
        Console.WriteLine();
    }

    public sealed class OurClass
    {
        // parameterless
        public OurClass()
            : this(42, "Hello, World!")
        {
            Console.WriteLine("Parameterless constructor called.");
        }

        // with parameters
        public OurClass(int someInt, string someString)
            : this(someInt, someString, 1.337)
        {
            Console.WriteLine("Constructor with parameters called.");
        }

        // private
        private OurClass(int someInt, string someString, double anotherNumber)
        {
            Console.WriteLine("Private constructor called.");

            Console.WriteLine($"someInt: {someInt}");
            Console.WriteLine($"someString: {someString}");
            Console.WriteLine($"anotherNumber: {anotherNumber}");
        }
    }
}