using System.Reflection;

public sealed class ActivatorExamples
{
    public void Run()
    {
        Type type1 = typeof(CustomType1);
        //CustomType1 instance1 = Activator.CreateInstance(type1);
        //object instance1 = Activator.CreateInstance(type1);
        //CustomType1 instance1a = Activator.CreateInstance<CustomType1>();

        //Console.WriteLine($"Is Instance 1 CustomType1: {instance1 is CustomType1}");
        //Console.WriteLine($"Is Instance 1a CustomType1: {instance1a is CustomType1}");

        //var type2 = typeof(CustomType2);
        //var instance2 = Activator.CreateInstance(
        //    type2,
        //    "This is the parameter!",
        //    123);

        var type3 = typeof(CustomType3);
        //var instance3 = Activator.CreateInstance(type3, "This is the parameter!");
        var instance3 = Activator.CreateInstance(
            type3,
            BindingFlags.NonPublic |
            BindingFlags.Instance,
            null,
            ["This is the parameter!"],
            null);
        var instance4 = Activator.CreateInstance(
            type3,
            nonPublic: true);
    }
    public sealed class CustomType1
    {
        public CustomType1()
        {
            Console.WriteLine("CustomType1 constructor");
        }
    }

    public sealed class CustomType2
    {
        public CustomType2(string parameter1, int extra)
        {
            Console.WriteLine("CustomType2 constructor");
            Console.WriteLine($"\tParameter1: {parameter1}");
        }
    }

    public sealed class CustomType3
    {
        private CustomType3()
        {
            Console.WriteLine("CustomType3 constructor without parameters");
        }

        private CustomType3(string parameter1)
        {
            Console.WriteLine("CustomType3 constructor");
            Console.WriteLine($"\tParameter1: {parameter1}");
        }
    }
}