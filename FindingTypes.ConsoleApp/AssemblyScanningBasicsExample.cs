using System.Reflection;

public sealed class AssemblyScanningBasicsExample
{
    public void Run()
    {
        // this is how we'd look at a single assembly
        var assembly = Assembly.GetExecutingAssembly();
        var types = assembly.GetTypes();

        // how do we check an entire directory though?
        static IReadOnlyList<Assembly> LoadAssembliesFromDirectory(string directoryPath)
        {
            var assemblies = new List<Assembly>();
            foreach (var file in Directory.EnumerateFiles(directoryPath, "*.dll"))
            {
                var assembly = Assembly.LoadFrom(file);
                assemblies.Add(assembly);
            }

            return assemblies;
        }

        static IReadOnlyList<Assembly> LoadAssembliesFromDirectory2(
            string directoryPath,
            string filter)
        {
            var assemblies = new List<Assembly>();
            foreach (var file in Directory.EnumerateFiles(directoryPath, filter))
            {
                var assembly = Assembly.LoadFrom(file);
                assemblies.Add(assembly);
            }

            return assemblies;
        }

        static IReadOnlyList<Assembly> LoadAssembliesFromDirectory3(
            string directoryPath,
            string filter)
        {
            var assemblies = new List<Assembly>();
            foreach (var file in Directory.EnumerateFiles(directoryPath, filter))
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

        var assemblies = LoadAssembliesFromDirectory3(
            ".",
            "*.dll");
        foreach (var asm in assemblies)
        {
            Console.WriteLine(asm.FullName);

            foreach (var type in asm.GetTypes())
            {
                Console.WriteLine($"\t{type.FullName}");
            }
        }
    }
}