using System.Reflection;
using System.Runtime.CompilerServices;

public sealed class FindingAttributesOnTypesExamples
{
    public void Run()
    {
        var typesThatHaveMyFancyAttribute = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(type => type
                .GetCustomAttributes<MyFancyAttribute>()
                .Any())
            .ToArray();

        Console.WriteLine($"Found {typesThatHaveMyFancyAttribute.Length} types with the MyFancyAttribute:");
        foreach (var t in typesThatHaveMyFancyAttribute)
        {
            var attribute = t.GetCustomAttribute<MyFancyAttribute>();
            Console.WriteLine($"Type: {t.Name}, Name: {attribute.Name}");
        }

        var type = typesThatHaveMyFancyAttribute.First();

        var methodsWithFancyAttributes = type
            .GetMethods()
            .Where(method => method
                .GetCustomAttributes<MyFancyAttribute>()
                .Any())
            .ToArray();

        Console.WriteLine($"Found {methodsWithFancyAttributes.Length} methods with the MyFancyAttribute:");
        foreach (var method in methodsWithFancyAttributes)
        {
            var attribute = method.GetCustomAttribute<MyFancyAttribute>();
            Console.WriteLine($"Method: {method.Name}, Name: {attribute.Name}");
        }

        var methodsWithCallerMemberNameParams = type
            .GetMethods()
            .Where(method => method
                .GetCustomAttributes<MyFancyAttribute>()
                .Any())
            .Where(method => method
                .GetParameters()
                .Any(param => param
                    .GetCustomAttributes<CallerMemberNameAttribute>()
                    .Any()))
            .ToArray();

        Console.WriteLine($"Found {methodsWithCallerMemberNameParams.Length} methods with the CallerMemberNameAttribute:");
        foreach (var method in methodsWithCallerMemberNameParams)
        {
            var parameter = method
                .GetParameters()
                .Single(param => param
                    .GetCustomAttributes<CallerMemberNameAttribute>()
                    .Any());
            Console.WriteLine($"Method: {method.Name}, Parameter: {parameter.Name}");
        }
    }

    [AttributeUsage(
        AttributeTargets.Class |
        AttributeTargets.Method)]
    public sealed class MyFancyAttribute : Attribute
    {
        public MyFancyAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    [MyFancyAttribute("This class has been annotated!")]
    public sealed class MyType
    {
        [MyFancyAttribute("This has caller member name!")]
        public void MyMethod([CallerMemberName] string? name = null)
        {
        }

        [MyFancyAttribute("This ALSO has a fancy name!")]
        public void MyMethod2(string? name = null)
        {
        }

        public void NotFancyAtAll(string? name = null)
        {
        }
    }
}