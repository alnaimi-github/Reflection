using BenchmarkDotNet.Attributes;

using FindingTypes.ExtraTypes;

using System.Reflection;

namespace Benchmarks.ConsoleApp;

[MemoryDiagnoser]
[ShortRunJob]
public class TypeDiscoveryBenchmarks
{
    private Assembly? _extraTypesAssembly;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _extraTypesAssembly = Assembly.GetAssembly(typeof(PublicClassWithNoMembers));
    }

    [Benchmark]
    public Type[] LoadFileAndGetTypes()
    {
        var assembly = Assembly.LoadFile(Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "FindingTypes.ExtraTypes.dll"));
        return assembly.GetTypes();
    }

    [Benchmark]
    public Type[] CachedAssemblyGetTypes()
    {
        return _extraTypesAssembly!.GetTypes();
    }

    [Benchmark(Baseline = true)]
    public Type[] TypeOfGetTypes()
    {
        return
        [
            typeof(PublicClassWithNoMembers),
            typeof(InternalClassWithNoMembers),
            typeof(PublicClassWithNestedClass),
            typeof(InternalClassWithNestedClass),
        ];
    }
}
