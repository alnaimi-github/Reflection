using System.Reflection;

public sealed class ExampleApp
{
    public void Run()
    {
        Console.WriteLine("Welcome to the Object Inspector!");

        object[] instances =
        [
            new TypeA(),
        new TypeB()
        ];

        while (true)
        {
            Console.WriteLine("Available objects to inspect:");

            for (int i = 0; i < instances.Length; i++)
            {
                Console.WriteLine($"{i}: {instances[i].GetType().Name}");
            }

            var indexUserInput = Ask("Enter the index of the object you want to inspect:");

            if (!int.TryParse(indexUserInput, out int objectInstance) ||
                objectInstance < 0 ||
                objectInstance >= instances.Length)
            {
                Error("Invalid input. Please try again.");
                continue;
            }

            var objectToInspect = instances[objectInstance];
            Console.WriteLine($"You selected: {objectToInspect.GetType().Name}");

            PrintAllInfo(objectToInspect);
            AskInvokeMethod(objectToInspect);
        }

        static void PrintAllInfo(object objectToInspect)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;

            PrintConstructorInfo(objectToInspect);
            PrintFieldInfo(objectToInspect);
            PrintPropertyInfo(objectToInspect);
            PrintMethodInfo(objectToInspect);

            Console.ForegroundColor = previousColor;
        }

        static void PrintConstructorInfo(object objectToInspect)
        {
            Console.WriteLine("Constructors:");
            var constructors = objectToInspect.GetType().GetConstructors(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);
            foreach (var constructor in constructors)
            {
                Console.WriteLine($"  Constructor:");
                Console.WriteLine($"    Parameters");

                var parameters = constructor.GetParameters();
                if (parameters.Length == 0)
                {
                    Console.WriteLine("      None!");
                }
                else
                {
                    foreach (var parameter in parameters)
                    {
                        Console.WriteLine($"      {parameter.ParameterType.Name} {parameter.Name}");
                    }
                }
            }
        }

        static void PrintFieldInfo(object objectToInspect)
        {
            Console.WriteLine("Fields:");
            var fields = objectToInspect.GetType().GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                Console.WriteLine($"  {field.FieldType.Name} {field.Name}");
                Console.WriteLine($"    Value: {field.GetValue(objectToInspect)}");
            }
        }

        static void PrintPropertyInfo(object objectToInspect)
        {
            Console.WriteLine("Properties:");
            var properties = objectToInspect.GetType().GetProperties(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);
            foreach (var property in properties)
            {
                Console.WriteLine($"  {property.PropertyType.Name} {property.Name}");
                Console.WriteLine($"    Value: {property.GetValue(objectToInspect)}");
            }
        }

        static void PrintMethodInfo(object objectToInspect)
        {
            Console.WriteLine("Methods:");
            var methods = objectToInspect.GetType().GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);
            foreach (var method in methods)
            {
                Console.WriteLine($"  {method.ReturnType.Name} {method.Name}");
                Console.WriteLine($"    Parameters");

                var parameters = method.GetParameters();
                if (parameters.Length == 0)
                {
                    Console.WriteLine("      None!");
                }
                else
                {
                    foreach (var parameter in parameters)
                    {
                        Console.WriteLine($"      {parameter.ParameterType.Name} {parameter.Name}");
                    }
                }
            }
        }

        static void AskInvokeMethod(object objectToInspect)
        {
            var methodName = Ask("Enter the name of the method you want to invoke (or empty to skip):");
            if (string.IsNullOrWhiteSpace(methodName))
            {
                return;
            }

            var method = objectToInspect.GetType().GetMethod(
                methodName,
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic);
            if (method == null)
            {
                Error("Method not found.");
                return;
            }

            var parameters = method.GetParameters();
            object?[] parameterValues = new object[parameters.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                var parameterValue = Ask(
                    $"Enter the value for parameter " +
                    $"{parameters[i].Name} ({parameters[i].ParameterType.Name}):");
                parameterValues[i] = Convert.ChangeType(
                    parameterValue,
                    parameters[i].ParameterType);
            }

            object? result = method.Invoke(objectToInspect, parameterValues);

            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Method {methodName} invoked. Result: {result}");
            Console.ForegroundColor = previousColor;
        }

        static string? Ask(string message)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            var input = Console.ReadLine();
            Console.ForegroundColor = previousColor;

            return input;
        }

        static void Error(string message)
        {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = previousColor;
        }
    }

    public sealed class TypeA
    {
        private readonly string _devLeader = "Dev Leader";

        public int Id { get; set; } = 42_1337;
    }

    public sealed class TypeB
    {

    }
}