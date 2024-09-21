using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

using System.Reflection;
using System.Runtime.CompilerServices;

BenchmarkSwitcher.FromAssembly(Assembly.GetExecutingAssembly()).Run(args);

public class OurType
{
    public static int StaticField;
    public int InstanceField;

    public static int StaticMethod()
    {
        return StaticField;
    }

    public int InstanceMethod()
    {
        return InstanceField;
    }

    public static int StaticProperty { get; set; }

    public int InstanceProperty { get; set; }
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_InstantiationBenchmarks
{
    [Benchmark(Baseline = true)]
    public OurType Constructor_Classic()
    {
        return new OurType();
    }

    [Benchmark]
    public OurType Constructor_Unsafe()
    {
        return UnsafeConstructor();
    }

    [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
    extern static OurType UnsafeConstructor();
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_GetFieldBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
    }

    [Benchmark(Baseline = true)]
    public int InstanceField_Classic()
    {
        return _instance!.InstanceField;
    }

    [Benchmark]
    public int InstanceField_Unsafe()
    {
        ref int instanceField = ref GetSetInstanceField(_instance!);
        return instanceField;
    }

    [Benchmark]
    public int StaticField_Classic()
    {
        return OurType.StaticField;
    }

    [Benchmark]
    public int StaticField_Unsafe()
    {
        ref int staticField = ref GetSetStaticField(null);
        return staticField;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "InstanceField")]
    extern static ref int GetSetInstanceField(
        OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "StaticField")]
    extern static ref int GetSetStaticField(
        OurType instance);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_SetFieldBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
    }

    [Benchmark(Baseline = true)]
    public void InstanceField_Classic()
    {
        _instance!.InstanceField = 123456;
    }

    [Benchmark]
    public void InstanceField_Unsafe()
    {
        ref int instanceField = ref GetSetInstanceField(_instance!);
        instanceField = 123456;
    }

    [Benchmark]
    public void StaticField_Classic()
    {
        OurType.StaticField = 123456;
    }

    [Benchmark]
    public void StaticField_Unsafe()
    {
        ref int staticField = ref GetSetStaticField(null);
        staticField = 123456;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "InstanceField")]
    extern static ref int GetSetInstanceField(
        OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "StaticField")]
    extern static ref int GetSetStaticField(
        OurType instance);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_GetPropertyBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
    }

    [Benchmark(Baseline = true)]
    public int InstanceProperty_Classic()
    {
        return _instance!.InstanceProperty;
    }

    [Benchmark]
    public int InstanceProperty_Unsafe()
    {
        return GetInstanceProperty(_instance!);
    }

    [Benchmark]
    public int StaticProperty_Classic()
    {
        return OurType.StaticProperty;
    }

    [Benchmark]
    public int StaticProperty_Unsafe()
    {
        return GetStaticProperty(null);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_InstanceProperty")]
    extern static int GetInstanceProperty(
        OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "get_StaticProperty")]
    extern static int GetStaticProperty(
        OurType instance);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_SetPropertyBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
    }

    [Benchmark(Baseline = true)]
    public void InstanceProperty_Classic()
    {
        _instance!.InstanceProperty = 123456;
    }

    [Benchmark]
    public void InstanceProperty_Unsafe()
    {
        SetInstanceProperty(_instance!, 123456);
    }

    [Benchmark]
    public void StaticProperty_Classic()
    {
        OurType.StaticProperty = 123456;
    }

    [Benchmark]
    public void StaticProperty_Unsafe()
    {
        SetStaticProperty(null, 123456);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "set_InstanceProperty")]
    extern static void SetInstanceProperty(
        OurType instance,
        int value);

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "set_StaticProperty")]
    extern static void SetStaticProperty(
        OurType instance,
        int value);
}

[ShortRunJob]
[MemoryDiagnoser]
public class UnsafeAccessors_MethodBenchmarks
{
    private OurType? _instance;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _instance = new OurType();
    }

    [Benchmark(Baseline = true)]
    public int InstanceMethod_Classic()
    {
        return _instance!.InstanceMethod();
    }

    [Benchmark]
    public int InstanceMethod_Unsafe()
    {
        return InvokeMethod(_instance!);
    }

    [Benchmark]
    public int StaticMethod_Classic()
    {
        return OurType.StaticMethod();
    }

    [Benchmark]
    public int StaticMethod_Unsafe()
    {
        return InvokeStaticMethod(null);
    }

    [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "InstanceMethod")]
    extern static int InvokeMethod(
        OurType instance);

    [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "StaticMethod")]
    extern static int InvokeStaticMethod(
        OurType instance);
}