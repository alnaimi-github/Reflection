using BenchmarkDotNet.Attributes;

using System.Reflection;

namespace Benchmarks.ConsoleApp;
public class ParameterlessClass
{
}

public class ClassicStringParameterClass
{
    private readonly string _value;

    public ClassicStringParameterClass(string value)
    {
        _value = value;
    }
}

public class PrimaryConstructorStringParameterClass(
    string _value)
{
}

[ShortRunJob]
public class Instantiation_ParameterlessClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(ParameterlessClass);
        _constructorInfo = _type.GetConstructor(Type.EmptyTypes);
    }

    [Benchmark(Baseline = true)]
    public ParameterlessClass Constructor()
    {
        return new ParameterlessClass();
    }

    [Benchmark]
    public object? Activator_Create_Instance()
    {
        return Activator.CreateInstance(_type!);
    }

    [Benchmark]
    public object? Type_Invoke_Member()
    {
        return _type!.InvokeMember(
            null,
            BindingFlags.CreateInstance,
            null,
            null,
            null);
    }

    [Benchmark]
    public object Constructor_Info_Invoke()
    {
        return _constructorInfo!.Invoke(null);
    }

    [Benchmark]
    public object Find_Constructor_Info_Then_Invoke()
    {
        var constructorInfo = _type.GetConstructor(Type.EmptyTypes);
        return constructorInfo!.Invoke(null);
    }
}

[ShortRunJob]
public class Instantiation_ClassicStringParameterClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(ClassicStringParameterClass);
        _constructorInfo = _type.GetConstructor([typeof(string)]);
    }

    [Benchmark(Baseline = true)]
    public ClassicStringParameterClass Constructor()
    {
        return new ClassicStringParameterClass("Hello World!");
    }

    [Benchmark]
    public object? Activator_Create_Instance()
    {
        return Activator.CreateInstance(
            _type!,
            new[]
            {
                "Hello World!",
            });
    }

    [Benchmark]
    public object? Type_Invoke_Member()
    {
        return _type!
            .InvokeMember(
                null,
                BindingFlags.CreateInstance,
                null,
                null,
                new[]
                {
                    "Hello World!",
                });
    }

    [Benchmark]
    public object? Constructor_Info_Invoke()
    {
        return _constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }

    [Benchmark]
    public object? Find_Constructor_Info_Then_Invoke()
    {
        var constructorInfo = _type.GetConstructor([typeof(string)]);
        return constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }
}

[ShortRunJob]
public class Instantiation_PrimaryConstructorStringParameterClassBenchmarks
{
    private Type? _type;
    private ConstructorInfo? _constructorInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _type = typeof(PrimaryConstructorStringParameterClass);
        _constructorInfo = _type.GetConstructor([typeof(string)]);
    }

    [Benchmark(Baseline = true)]
    public PrimaryConstructorStringParameterClass Constructor()
    {
        return new PrimaryConstructorStringParameterClass("Hello World!");
    }

    [Benchmark]
    public object? Activator_Create_Instance()
    {
        return Activator.CreateInstance(
            _type!,
            new[]
            {
                "Hello World!",
            });
    }

    [Benchmark]
    public object? Type_Invoke_Member()
    {
        return _type!
            .InvokeMember(
                null,
                BindingFlags.CreateInstance,
                null,
                null,
                new[]
                {
                    "Hello World!",
                });
    }

    [Benchmark]
    public object? Constructor_Info_Invoke()
    {
        return _constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }

    [Benchmark]
    public object? Find_Constructor_Info_Then_Invoke()
    {
        var constructorInfo = _type.GetConstructor([typeof(string)]);
        return constructorInfo!.Invoke(new[]
        {
            "Hello World!",
        });
    }
}