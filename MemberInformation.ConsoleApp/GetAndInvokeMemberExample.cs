using System.Reflection;

public sealed class GetAndInvokeMemberExample
{
    public void Run()
    {
        BindingFlags searchBindingFlags =
            BindingFlags.Public |
            BindingFlags.Instance;
        Type type = typeof(MyType);

        var myConstructorMember = type.GetMember(".ctor", searchBindingFlags).Single();
        Console.WriteLine($"My Constructor Member: {myConstructorMember.Name}");

        var myMessageMember = type.GetMember("MyMessage", searchBindingFlags).Single();
        Console.WriteLine($"My Message Member: {myMessageMember.Name}");

        var myMethodMember = type.GetMember("MyMethod", searchBindingFlags).Single();
        Console.WriteLine($"My Method Member: {myMethodMember.Name}");

        var instance = type.InvokeMember(
            name: null,
            invokeAttr: searchBindingFlags | BindingFlags.CreateInstance,
            binder: null,
            target: null,
            args: ["Hello, World!"]);

        type.InvokeMember(
            name: "MyMethod",
            invokeAttr: searchBindingFlags | BindingFlags.InvokeMethod,
            binder: null,
            target: instance,
            args: null);

        var message = (string?)type.InvokeMember(
            name: "MyMessage",
            invokeAttr: searchBindingFlags | BindingFlags.GetProperty,
            binder: null,
            target: instance,
            args: null);
        Console.WriteLine($"My Message: {message}");
    }

    public sealed class MyType
    {
        private string _myMessage;

        public MyType(string myMessage)
        {
            _myMessage = myMessage;
        }

        public string MyMessage => _myMessage;

        public void MyMethod()
        {
            Console.WriteLine(_myMessage);
        }
    }
}