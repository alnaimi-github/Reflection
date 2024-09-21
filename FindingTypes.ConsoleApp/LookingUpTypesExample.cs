//using System.Reflection;

//public sealed class LookingUpTypesExample
//{
//    public void Run()
//    {
//        Console.WriteLine("Type by typeof:");
//        var typeofA = typeof(MyTypeA);
//        var typeofB = typeof(MyTypeB);
//        Console.WriteLine(typeofA.AssemblyQualifiedName);
//        Console.WriteLine(typeofB.AssemblyQualifiedName);
//        Console.WriteLine();

//        Console.WriteLine("Type by name:");
//        var typeCByName = Type.GetType("MyTypeC");
//        var typeDByName = Type.GetType("MyTypeC+MyTypeD");
//        var typeEByName = Type.GetType("MyTypeC+MyTypeE");
//        Console.WriteLine(typeCByName.AssemblyQualifiedName);
//        Console.WriteLine(typeDByName.AssemblyQualifiedName);
//        Console.WriteLine(typeEByName.AssemblyQualifiedName);
//        Console.WriteLine();

//        Console.WriteLine("Type by name vs namespace:");
//        var typeAByName = Type.GetType("MyTypeA");
//        var typeAFromCoolNamespace = Type.GetType("CoolNamespace.MyTypeA");
//        Console.WriteLine(typeAByName.AssemblyQualifiedName);
//        Console.WriteLine(typeAFromCoolNamespace.AssemblyQualifiedName);
//        Console.WriteLine();

//        Console.WriteLine("Type from assembly:");
//        var typeAFromAssembly = Assembly.GetExecutingAssembly().GetType("MyTypeA");
//        Console.WriteLine(typeAFromAssembly.AssemblyQualifiedName);
//        Console.WriteLine();

//        Console.WriteLine("All types from assembly:");
//        var allTypesFromAssembly = Assembly.GetExecutingAssembly().GetTypes();
//        foreach (var type in allTypesFromAssembly)
//        {
//            Console.WriteLine(type.FullName);
//        }

//        Console.ReadLine();
//    }
//}

//public sealed class MyTypeA
//{
//}

//internal sealed class MyTypeB
//{
//}

//public sealed class MyTypeC
//{
//    public sealed class MyTypeD
//    {
//    }

//    private sealed class MyTypeE
//    {
//    }
//}

//namespace CoolNamespace
//{
//    public sealed class MyTypeA
//    {
//    }
//}
