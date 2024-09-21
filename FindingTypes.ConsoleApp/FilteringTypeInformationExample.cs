using System.Reflection;

public sealed class FilteringTypeInformationExample
{
    public void Run()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var allTypes = assembly.GetTypes();

        Console.WriteLine("All abstract types:");
        foreach (var type in allTypes.Where(x => x.IsAbstract))
        {
            Console.WriteLine(type.Name);
        }
        Console.WriteLine();

        Console.WriteLine("All sealed types:");
        foreach (var type in allTypes.Where(x => x.IsSealed))
        {
            Console.WriteLine(type.Name);
        }
        Console.WriteLine();

        Console.WriteLine("All interfaces:");
        foreach (var type in allTypes.Where(x => x.IsInterface))
        {
            Console.WriteLine(type.Name);
        }
        Console.WriteLine();

        MyDerivedTypeA a = new MyDerivedTypeA();
        bool isInstanceOf = a is MyBaseType;

        Type typeA = typeof(MyDerivedTypeA);
        typeA = a.GetType();

        var canAssignToBaseA = typeA.IsAssignableTo(typeof(MyBaseType));
        var canAssignToInterfaceA = typeA.IsAssignableTo(typeof(IMyInterface));

        Type typeB = typeof(MyDerivedTypeB);
        var canAssignToBaseB = typeB.IsAssignableTo(typeof(MyBaseType));
        var canAssignToInterfaceB = typeB.IsAssignableTo(typeof(IMyInterface));

        Console.WriteLine("Can MyDerivedTypeA be assigned to MyBaseType? " + canAssignToBaseA);
        Console.WriteLine("Can MyDerivedTypeA be assigned to IMyInterface? " + canAssignToInterfaceA);
        Console.WriteLine("Can MyDerivedTypeB be assigned to MyBaseType? " + canAssignToBaseB);
        Console.WriteLine("Can MyDerivedTypeB be assigned to IMyInterface? " + canAssignToInterfaceB);

        Console.WriteLine("All types that implement IMyInterface:");
        foreach (var type in allTypes.Where(x => x.IsAssignableTo(typeof(IMyInterface))))
        {
            Console.WriteLine(type.Name);
        }
        Console.WriteLine();

        Console.WriteLine("All types that derive from MyBaseType:");
        foreach (var type in allTypes.Where(x => x.IsAssignableTo(typeof(MyBaseType))))
        {
            Console.WriteLine(type.Name);
        }
        Console.WriteLine();
    }

    public abstract class MyBaseType
    {

    }

    public sealed class MyDerivedTypeA :
        MyBaseType
    {
    }

    public sealed class MyDerivedTypeB :
        MyBaseType,
        IMyInterface
    {
    }

    public interface IMyInterface
    {
    }
}