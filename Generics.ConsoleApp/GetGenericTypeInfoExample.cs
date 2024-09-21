using System.Reflection;

public sealed class GetGenericTypeInfoExample
{
    public void Run()
    {
        var genericTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsGenericType)
            .ToArray();

        Console.WriteLine("The generic types in this assembly are:");
        foreach (var genericType in genericTypes)
        {
            Console.WriteLine(genericType.Name);

            Console.WriteLine("  Type Parameters:");
            var genericTypeParameters = genericType.GetGenericArguments();
            foreach (var genericTypeParameter in genericTypeParameters)
            {
                Console.WriteLine($"    {genericTypeParameter.Name}");

                var constraints = genericTypeParameter.GetGenericParameterConstraints();
                foreach (var constraint in constraints)
                {
                    Console.WriteLine($"    * Must Be: {constraint.Name}");
                }
            }

            Console.WriteLine("  Members:");
            foreach (var member in genericType.GetMembers())
            {
                Console.WriteLine($"    {member.Name}");

                if (member is MethodInfo method)
                {
                    var methodParameters = method
                        .GetParameters();
                    foreach (var methodParameter in methodParameters)
                    {
                        Console.WriteLine(
                            $"      " +
                            $"{methodParameter.Name}: " +
                            $"{methodParameter.ParameterType.Name}");

                        if (methodParameter.ParameterType.IsGenericParameter)
                        {
                            var constraints = methodParameter
                                .ParameterType
                                .GetGenericParameterConstraints();
                            foreach (var constraint in constraints)
                            {
                                Console.WriteLine($"        * Must Be: {constraint.Name}");
                            }
                        }
                    }
                }
            }
        }
    }
}














//var genericMethods = genericTypeA
//    .GetMethods()
//    .Where(m => m.IsGenericMethod)
//    .ToArray();

//Console.WriteLine("Generic Methods:");
//foreach (var genericMethod in genericMethods)
//{
//    Console.WriteLine(genericMethod.Name);

//    var genericMethodParameters = genericMethod.GetGenericArguments();
//    foreach (var genericMethodParameter in genericMethodParameters)
//    {
//        Console.WriteLine($"  {genericMethodParameter.Name}");

//        var constraints = genericMethodParameter.GetGenericParameterConstraints();
//        foreach (var constraint in constraints)
//        {
//            Console.WriteLine($"    Must Be: {constraint.Name}");
//        }
//    }
//}

//GenericTypeA<int, SomeImplementation, string> instance = new();

//var method1 = instance.GetType().GetMethod("Method1");
//method1.Invoke(instance, [42, new SomeImplementation("Some input!")]);

//var method2 = instance.GetType().GetMethod("Method2");
////method2.Invoke(instance, new object[] { new SomeImplementation("Some input!") });
//method2 = method2.MakeGenericMethod(typeof(SomeOtherImplementation));
//method2.Invoke(instance, [new SomeOtherImplementation(42)]);