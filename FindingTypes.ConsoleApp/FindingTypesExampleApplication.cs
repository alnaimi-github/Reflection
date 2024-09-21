using System.Reflection;

public sealed class FindingTypesExampleApplication
{
    public static void Run()
    {
        string? targetPath;
        while (true)
        {
            Console.WriteLine("Enter the target path to scan either:");
            Console.WriteLine("\tThe single target assembly file");
            Console.WriteLine("\tAll assemblies in the directory");

            targetPath = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(targetPath))
            {
                Console.WriteLine("Invalid target path!");
                continue;
            }

            break;
        }

        var assemblies = Directory.Exists(targetPath)
            ? LoadAssembliesFromDirectory(targetPath)
            : LoadAssembliesFromFile(targetPath);

        Console.WriteLine($"Examining {assemblies.Count} assemblies...");
        Dictionary<Assembly, IReadOnlyList<Type>> typesByAssembly = new(assemblies.Count);
        foreach (var assembly in assemblies)
        {
            var types = assembly.GetTypes();
            typesByAssembly[assembly] = types;
        }

        var totalTypes = typesByAssembly.Values.Sum(types => types.Count);
        Console.WriteLine($"There are {totalTypes} types.");

        while (true)
        {
            var filter = AskForFilter();
            if (filter is null)
            {
                Console.WriteLine("Invalid selection!");
                continue;
            }

            foreach (var kvp in typesByAssembly)
            {
                var resultsPerAssembly = kvp.Value.Where(x => filter(x)).ToArray();
                if (resultsPerAssembly.Length == 0)
                {
                    continue;
                }

                Console.WriteLine($"Assembly: {kvp.Key.FullName}");
                foreach (var type in resultsPerAssembly)
                {
                    Console.WriteLine($"\tType: {type.FullName}");
                }
            }
        }
    }

    private static Predicate<Type>? AskForFilter()
    {
        Console.WriteLine("Enter the filter type:");
        Console.WriteLine("\tNamespace");
        Console.WriteLine("\tType Name");
        Console.WriteLine("\tVisibility");

        var filterType = Console.ReadLine();

        var filter = filterType switch
        {
            "Namespace" => AskForNamespaceFilter(),
            "Type Name" => AskForTypeNameFilter(),
            "Visibility" => AskForVisibilityFilter(),
            _ => null
        };

        return filter;
    }

    private static Predicate<Type>? AskForNamespaceFilter()
    {
        Console.WriteLine("Enter the part of the namespace to match against:");
        var namespaceFilter = Console.ReadLine();

        // NOTE: we can allow empty namespace :)
        return type => 
            (string.IsNullOrEmpty(type.Namespace) && string.IsNullOrEmpty(namespaceFilter)) ||
            (!string.IsNullOrEmpty(namespaceFilter) && type.Namespace?.Contains(namespaceFilter, StringComparison.OrdinalIgnoreCase) == true);
    }

    private static Predicate<Type>? AskForTypeNameFilter()
    {
        Console.WriteLine("Enter the string to match against the full type name:");
        var typeNameFilter = Console.ReadLine();

        // allow match-all
        if (string.IsNullOrEmpty(typeNameFilter))
        {
            return type => true;
        }

        return type => type.FullName?.Contains(typeNameFilter, StringComparison.OrdinalIgnoreCase) == true;
    }

    private static Predicate<Type>? AskForVisibilityFilter()
    {
        Console.WriteLine("Enter the visibility:");
        Console.WriteLine("\tPublic");
        Console.WriteLine("\tNot Public");

        var visibilityFilter = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(visibilityFilter))
        {
            return null;
        }

        return visibilityFilter switch
        {
            "Public" => type => type.IsPublic,
            "Not Public" => type => !type.IsPublic,
            _ => null
        };
    }

    private static IReadOnlyList<Assembly> LoadAssembliesFromDirectory(string directoryPath)
    {
        var assemblies = new List<Assembly>();
        foreach (var file in Directory.EnumerateFiles(directoryPath, "*.dll"))
        {
            try
            {
                var assembly = Assembly.LoadFrom(file);
                assemblies.Add(assembly);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load assembly from {file}: {ex.Message}");
            }
        }

        return assemblies;
    }

    private static IReadOnlyList<Assembly> LoadAssembliesFromFile(string filePath)
    {
        try
        {
            var assembly = Assembly.LoadFrom(filePath);
            return [assembly];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load assembly from {filePath}: {ex.Message}");
            return [];
        }
    }
}