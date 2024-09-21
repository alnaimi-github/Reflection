using BenchmarkDotNet.Attributes;

using System.Reflection;

namespace Benchmarks.ConsoleApp;

public sealed class ClassToInvoke
{
    public static int StaticPublicField;

    public int PublicField;

    public static int StaticPublicProperty { get; set; }

    public int PublicProperty { get; set; }

    public static void StaticPublicMethod()
    {
    }

    public void PublicMethod()
    {
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_GetInstanceFieldBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private FieldInfo? _fieldInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
         _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _fieldInfo = _targetType.GetField("PublicField");
    }

    [Benchmark(Baseline = true)]
    public int GetField_NoReflection()
    {
        return _instance!.PublicField;
    }

    [Benchmark]
    public int GetField_CachedFieldInfo()
    {
        return (int)_fieldInfo!.GetValue(_instance)!;
    }

    [Benchmark]
    public int GetField_FieldInfo()
    {
        var fieldInfo = _targetType!.GetField("PublicField");
        return (int)fieldInfo!.GetValue(_instance)!;
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_GetStaticFieldBenchmarks
{
    private Type? _targetType;
    private FieldInfo? _fieldInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _fieldInfo = _targetType.GetField("StaticPublicField");
    }

    [Benchmark(Baseline = true)]
    public int GetField_NoReflection()
    {
        return ClassToInvoke.StaticPublicField;
    }

    [Benchmark]
    public int GetField_CachedFieldInfo()
    {
        return (int)_fieldInfo!.GetValue(null)!;
    }

    [Benchmark]
    public int GetField_FieldInfo()
    {
        var fieldInfo = _targetType!.GetField("StaticPublicField");
        return (int)fieldInfo!.GetValue(null)!;
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_GetInstancePropertyBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private PropertyInfo? _propertyInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _propertyInfo = _targetType.GetProperty("PublicProperty");
    }

    [Benchmark(Baseline = true)]
    public int GetProperty_NoReflection()
    {
        return _instance!.PublicProperty;
    }

    [Benchmark]
    public int GetProperty_CachedPropertyInfo()
    {
        return (int)_propertyInfo!.GetValue(_instance)!;
    }

    [Benchmark]
    public int GetProperty_PropertyInfo()
    {
        var propertyInfo = _targetType!.GetProperty("PublicProperty");
        return (int)propertyInfo!.GetValue(_instance)!;
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_GetStaticPropertyBenchmarks
{
    private Type? _targetType;
    private PropertyInfo? _propertyInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _propertyInfo = _targetType.GetProperty("StaticPublicProperty");
    }

    [Benchmark(Baseline = true)]
    public int GetProperty_NoReflection()
    {
        return ClassToInvoke.StaticPublicProperty;
    }

    [Benchmark]
    public int GetProperty_CachedPropertyInfo()
    {
        return (int)_propertyInfo!.GetValue(null)!;
    }

    [Benchmark]
    public int GetProperty_PropertyInfo()
    {
        var propertyInfo = _targetType!.GetProperty("StaticPublicProperty");
        return (int)propertyInfo!.GetValue(null)!;
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_SetInstanceFieldBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private FieldInfo? _fieldInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _fieldInfo = _targetType.GetField("PublicField");
    }

    [Benchmark(Baseline = true)]
    public void SetField_NoReflection()
    {
        _instance!.PublicField = 123456;
    }

    [Benchmark]
    public void SetField_CachedFieldInfo()
    {
        _fieldInfo!.SetValue(_instance, 123456);
    }

    [Benchmark]
    public void SetField_FieldInfo()
    {
        var fieldInfo = _targetType!.GetField("PublicField");
        fieldInfo!.SetValue(_instance, 123456);
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_SetStaticFieldBenchmarks
{
    private Type? _targetType;
    private FieldInfo? _fieldInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _fieldInfo = _targetType.GetField("StaticPublicField");
    }

    [Benchmark(Baseline = true)]
    public void SetField_NoReflection()
    {
        ClassToInvoke.StaticPublicField = 123456;
    }

    [Benchmark]
    public void SetField_CachedFieldInfo()
    {
        _fieldInfo!.SetValue(null, 123456);
    }

    [Benchmark]
    public void SetField_FieldInfo()
    {
        var fieldInfo = _targetType!.GetField("StaticPublicField");
        fieldInfo!.SetValue(null, 123456);
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_SetInstancePropertyBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private PropertyInfo? _propertyInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _propertyInfo = _targetType.GetProperty("PublicProperty");
    }

    [Benchmark(Baseline = true)]
    public void SetProperty_NoReflection()
    {
        _instance!.PublicProperty = 123456;
    }

    [Benchmark]
    public void SetProperty_CachedPropertyInfo()
    {
        _propertyInfo!.SetValue(_instance, 123456);
    }

    [Benchmark]
    public void SetProperty_PropertyInfo()
    {
        var propertyInfo = _targetType!.GetProperty("PublicProperty");
        propertyInfo!.SetValue(_instance, 123456);
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_SetStaticPropertyBenchmarks
{
    private Type? _targetType;
    private PropertyInfo? _propertyInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _propertyInfo = _targetType.GetProperty("StaticPublicProperty");
    }

    [Benchmark(Baseline = true)]
    public void SetProperty_NoReflection()
    {
        ClassToInvoke.StaticPublicProperty = 123456;
    }

    [Benchmark]
    public void SetProperty_CachedPropertyInfo()
    {
        _propertyInfo!.SetValue(null, 123456);
    }

    [Benchmark]
    public void SetProperty_PropertyInfo()
    {
        var propertyInfo = _targetType!.GetProperty("StaticPublicProperty");
        propertyInfo!.SetValue(null, 123456);
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_CallInstanceMethodBenchmarks
{
    private Type? _targetType;
    private ClassToInvoke? _instance;
    private MethodInfo? _methodInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _instance = new ClassToInvoke();
        _methodInfo = _targetType.GetMethod("PublicMethod");
    }

    [Benchmark(Baseline = true)]
    public void CallMethod_NoReflection()
    {
        _instance!.PublicMethod();
    }

    [Benchmark]
    public void CallMethod_CachedMethodInfo()
    {
        _methodInfo!.Invoke(_instance, null);
    }

    [Benchmark]
    public void CallMethod_MethodInfo()
    {
        var methodInfo = _targetType!.GetMethod("PublicMethod")!;
        methodInfo.Invoke(_instance, null);
    }
}

[MemoryDiagnoser]
[ShortRunJob]
public class MemberInvocation_CallStaticMethodBenchmarks
{
    private Type? _targetType;
    private MethodInfo? _methodInfo;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _targetType = typeof(ClassToInvoke);
        _methodInfo = _targetType.GetMethod("StaticPublicMethod");
    }

    [Benchmark(Baseline = true)]
    public void CallMethod_NoReflection()
    {
        ClassToInvoke.StaticPublicMethod();
    }

    [Benchmark]
    public void CallMethod_CachedMethodInfo()
    {
        _methodInfo!.Invoke(null, null);
    }

    [Benchmark]
    public void CallMethod_MethodInfo()
    {
        var methodInfo = _targetType!.GetMethod("StaticPublicMethod")!;
        methodInfo.Invoke(null, null);
    }
}