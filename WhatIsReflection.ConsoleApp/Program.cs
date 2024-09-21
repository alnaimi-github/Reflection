NicksType myVariable = new NicksType()
{
    Value = 1,
    Name = "Nick"
};

var type = myVariable.GetType();
Console.Write(type.IsSealed);

public sealed class NicksType
{
    public int Value { get; set; }

    public string Name { get; set; }
}