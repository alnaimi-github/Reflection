using System.Reflection;

public sealed class ExampleApplication
{
    public void Run()
    {
        var constructableTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.GetCustomAttribute<ConstructableAttribute>() != null)
            .ToArray();
        Console.WriteLine(
            $"Found {constructableTypes.Length} types with the [Constructable] attribute.");

        while (true)
        {
            var typeToCreate = AskForType(constructableTypes);
            if (typeToCreate is null)
            {
                continue;
            }

            var activationMethod = AskForActivationMethod();
            if (!activationMethod.HasValue)
            {
                continue;
            }

            var instance = activationMethod switch
            {
                1 => ActivatorCreate(typeToCreate),
                2 => ConstructorInfoCreate(typeToCreate),
                _ => FailActivationMethod(activationMethod.Value),
            };

            if (instance is null)
            {
                Console.WriteLine("Failed to create an instance.");
                continue;
            }

            Console.WriteLine($"Created an instance of '{typeToCreate.Name}'!");
        }

        static object? FailActivationMethod(int activationMethod)
        {
            Console.WriteLine($"Invalid activation method '{activationMethod}'.");
            return null;
        }

        static object? ActivatorCreate(Type type)
        {
            while (true)
            {
                var arguments = AskForArguments(type);
                if (arguments is null)
                {
                    continue;
                }

                Console.WriteLine(
                    $"Creating an instance of '{type.Name}' with Activator.CreateInstance...");
                return Activator.CreateInstance(type, arguments);
            }
        }

        static object? ConstructorInfoCreate(Type type)
        {
            while (true)
            {
                var arguments = AskForArguments(type);
                if (arguments is null)
                {
                    continue;
                }

                Console.WriteLine(
                    $"Creating an instance of '{type.Name}' with ConstructorInfo...");
                var constructorInfo = type.GetConstructors().Single();
                return constructorInfo.Invoke(arguments);
            }
        }

        static Type? AskForType(Type[] constructableTypes)
        {
            Console.WriteLine("Available type names are:");
            foreach (var type in constructableTypes)
            {
                Console.WriteLine($"  {type.Name}");
            }

            Console.WriteLine("Enter the name of the type to create an instance of:");
            var typeNameUserInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(typeNameUserInput))
            {
                return null;
            }

            var typeToCreate = constructableTypes
                .FirstOrDefault(t => string
                .Equals(
                    t.Name,
                    typeNameUserInput,
                    StringComparison.OrdinalIgnoreCase));
            if (typeToCreate is null)
            {
                Console.WriteLine("Type not found. Check the spelling!");
                return null;
            }

            return typeToCreate;
        }

        static int? AskForActivationMethod()
        {
            while (true)
            {
                Console.WriteLine("Available activation methods:");
                Console.WriteLine("  1. Activator.CreateInstance");
                Console.WriteLine("  2. ConstructorInfo.Invoke");
                Console.WriteLine("Enter the number of the activation method to use:");
                var activationMethodUserInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(activationMethodUserInput))
                {
                    return null;
                }

                if (!int.TryParse(
                    activationMethodUserInput,
                    out var activationMethodNumber))
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                    continue;
                }

                return activationMethodNumber;
            }
        }

        static object?[]? AskForArguments(Type type)
        {
            var constructors = type.GetConstructors();
            if (constructors.Length == 0)
            {
                Console.WriteLine("No public constructors found.");
                return null;
            }

            if (constructors.Length != 1)
            {
                Console.WriteLine("Multiple constructors found.");
                return null;
            }

            var constructor = constructors[0];
            var parameters = constructor.GetParameters();

            while (true)
            {
                var arguments = new object?[parameters.Length];
                int argumentIndex = -1;
                bool argumentCollectionFailed = false;

                foreach (var parameter in parameters)
                {
                    argumentIndex++;

                    if (parameter.ParameterType.Equals(typeof(int)))
                    {
                        Console.WriteLine($"Enter an integer value for '{parameter.Name}':");
                        var userInput = Console.ReadLine();
                        if (!int.TryParse(userInput, out var value))
                        {
                            Console.WriteLine("Invalid input. Please enter an integer.");
                            argumentCollectionFailed = true;
                            break;
                        }

                        arguments[argumentIndex] = value;
                    }
                    else if (parameter.ParameterType.Equals(typeof(string)))
                    {
                        Console.WriteLine($"Enter a string value for '{parameter.Name}':");
                        var value = Console.ReadLine();
                        arguments[argumentIndex] = value;
                    }
                    else
                    {
                        // TODO: how might you expand this even further for more support?
                        Console.WriteLine(
                            $"Unsupported parameter type '{parameter.ParameterType.Name}'.");
                        return null;
                    }
                }

                if (argumentCollectionFailed)
                {
                    continue;
                }

                return arguments;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ConstructableAttribute : Attribute
    {
    }

    [Constructable]
    public sealed class TypeA
    {
        public TypeA()
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Hello, from TypeA!");
            Console.ForegroundColor = currentColor;
        }
    }

    [Constructable]
    public sealed class TypeB
    {
        public TypeB(int value1, string value2)
        {
            var currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Hello, from TypeB!");
            Console.WriteLine($"  Value1: {value1}");
            Console.WriteLine($"  Value2: {value2}");
            Console.ForegroundColor = currentColor;
        }
    }
}