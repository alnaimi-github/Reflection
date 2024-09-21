using System.Reflection;

public sealed class GenericMethodCallExamples
{
    public void Run()
    {
        // to refer to a generic type without the type parameter:
        //var genericTypeA = typeof(GenericTypeA<,,>);
        var genericTypeA = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsGenericType)
            .SingleOrDefault(t => t.Name == "GenericTypeA`3");

        var genericMethods = genericTypeA
            .GetMethods()
            .Where(m => m.IsGenericMethod)
            .ToArray();

        GenericTypeA<int, SomeImplementation, string> instance = new();

        var method1 = instance.GetType().GetMethod("Method1");
        method1.Invoke(instance, [42, new SomeImplementation("Some input!")]);

        var method2 = instance.GetType().GetMethod("Method2");
        //method2.Invoke(instance, [new SomeOtherImplementation(123)]);
        method2 = method2.MakeGenericMethod(typeof(SomeOtherImplementation));
        method2.Invoke(instance, [new SomeOtherImplementation(42)]);
    }
}