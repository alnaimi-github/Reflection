using System.Reflection;

public sealed class InstantiationExample
{
    public void Run()
    {
        var genericTypeA = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsGenericType)
            .SingleOrDefault(t => t.Name == "GenericTypeA`3");

        //var instance = Activator.CreateInstance(genericTypeA);

        genericTypeA = genericTypeA.MakeGenericType(
            typeof(string),
            typeof(SomeImplementation),
            typeof(int));

        var instance = Activator.CreateInstance(genericTypeA);
        var specificInstance = (GenericTypeA<string, SomeImplementation, int>)instance;

        specificInstance.Method1(
            "Hello, World!",
            new SomeImplementation("From Dev Leader"));
    }
}