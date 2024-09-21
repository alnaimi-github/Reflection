using System.Reflection;

public sealed class ExampleApplication
{
    public void Run()
    {
        Console.WriteLine(
            "Enter the path to the assembly or leave empty for " +
            "the currently executing assembly.");
        var assemblyPath = Console.ReadLine();
        var assembly = string.IsNullOrWhiteSpace(assemblyPath)
            ? Assembly.GetExecutingAssembly()
            : Assembly.LoadFrom(assemblyPath);

        Console.WriteLine($"Assembly: {assembly.FullName}");
        foreach (var type in assembly.GetTypes())
        {
            bool writtenTypeHeader = false;
            void WriteTypeHeaderIfNeeded()
            {
                if (writtenTypeHeader)
                {
                    return;
                }

                Console.WriteLine($"Type");
                Console.WriteLine($"  Name: {type.FullName}");
                writtenTypeHeader = true;
            }

            var typeDocumentation = type.GetCustomAttribute<DocumentationAttribute>();
            if (typeDocumentation != null)
            {
                WriteTypeHeaderIfNeeded();
                Console.WriteLine($"  Description: {typeDocumentation.Description}");
            }

            bool writtenMembersHeader = false;
            void WriteMembersHeaderIfNeeded(MemberInfo member)
            {
                if (writtenMembersHeader)
                {
                    return;
                }

                WriteTypeHeaderIfNeeded();

                Console.WriteLine($"  Members:");
                writtenMembersHeader = true;
            }

            foreach (var member in type
                .GetMembers(
                    BindingFlags.Public |
                    //BindingFlags.NonPublic | 
                    BindingFlags.Instance |
                    BindingFlags.Static)
                .OrderBy(x => x.MemberType))
            {
                var memberDocumentation = member.GetCustomAttribute<DocumentationAttribute>();
                if (memberDocumentation != null)
                {
                    WriteMembersHeaderIfNeeded(member);

                    Console.WriteLine($"      Type: {member.MemberType}");
                    Console.WriteLine($"      Name: {member.Name}");
                    Console.WriteLine($"      Description: {memberDocumentation.Description}");
                }
            }
        }
    }

    [AttributeUsage(
        AttributeTargets.Method |
        AttributeTargets.Property |
        AttributeTargets.Constructor |
        AttributeTargets.Class)]
    public class DocumentationAttribute : Attribute
    {
        public DocumentationAttribute(string description) => Description = description;

        public string Description { get; }
    }

    [Documentation("This is a type.")]
    public sealed class MyType
    {
        [Documentation("This is a property.")]
        public string MyProperty { get; set; }

        [Documentation("This is a method.")]
        public void MyMethod()
        {
        }

        [Documentation("This is a constructor.")]
        public MyType()
        {
        }
    }
}