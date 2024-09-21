using System.Reflection;

public sealed class CallingMethodsExample
{
    public void Run()
    {
        // do we have the right binding flags?
        var bindingFlags =
            BindingFlags.Instance |
            BindingFlags.Public |
            BindingFlags.NonPublic;

        OurClass instance = new();
        var type = instance.GetType();

        var doSomething = type.GetMethod("DoSomething", bindingFlags);
        doSomething.Invoke(instance, null);

        var doSomethingElse = type.GetMethod("DoSomethingElse", bindingFlags);
        doSomethingElse.Invoke(instance, ["Hello, World!", 42]);

        var addNumbers = type.GetMethod("AddNumbers", bindingFlags);
        var result = addNumbers.Invoke(instance, [10, 20]);
        Console.WriteLine($"Result: {result}");
    }

    public sealed class OurClass
    {
        private void DoSomething()
        {
            Console.WriteLine($"This is the {nameof(DoSomething)} method!");
        }

        private void DoSomethingElse(
            string stringValue,
            int integerValue)
        {
            Console.WriteLine($"This is the {nameof(DoSomethingElse)} method!");
        }

        private static int AddNumbers(int numberA, int numberB)
        {
            return numberA + numberB;
        }
    }
}