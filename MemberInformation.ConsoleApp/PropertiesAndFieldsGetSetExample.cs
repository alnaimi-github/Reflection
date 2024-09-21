using System.Reflection;

public sealed class PropertiesAndFieldsGetSetExample
{
    public void Run()
    {
        OurClass instance = new();
        var type = instance.GetType();

        // what binding flags do we need to use to see all fields?
        var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Console.WriteLine($"There are {fields.Length} fields.");
        foreach (var field in fields)
        {
            Console.WriteLine($"\tField: {field}");
        }

        var properties = type.GetProperties();
        Console.WriteLine($"There are {properties.Length} properties.");
        foreach (var property in properties)
        {
            Console.WriteLine($"\tProperty: {property}");
        }


        //
        // Fields
        //
        var readOnlyField = fields.First(fields => fields.Name == "_readOnlyField");
        var readOnlyFieldValue = readOnlyField.GetValue(instance);
        Console.WriteLine($"The value of the read-only field is {readOnlyFieldValue}.");

        // ... let's come back to this at the end and see if we can break a rule...


        // how would we access the static field?

        //
        // Properties!
        //
        var readWriteAutoProperty = properties.First(properties => properties.Name == "ReadWriteAutoProperty");
        var readWriteAutoPropertyValue = readWriteAutoProperty.GetValue(instance);
        Console.WriteLine($"The value of the read-write auto property is {readWriteAutoPropertyValue}.");

        readWriteAutoProperty.SetValue(instance, 789);
        readWriteAutoPropertyValue = readWriteAutoProperty.GetValue(instance);
        Console.WriteLine($"The value of the read-write auto property is now {readWriteAutoPropertyValue}.");


        var readOverReadWriteField = properties.First(properties => properties.Name == "ReadOverReadWriteField");
        var readOverReadWriteFieldValue = readOverReadWriteField.GetValue(instance);
        Console.WriteLine($"The value of the read-over read-write field is {readOverReadWriteFieldValue}.");

        //readOverReadWriteField.SetValue(instance, 1337);


        var fullOverReadWriteField = properties.First(properties => properties.Name == "FullOverReadWriteField");
        var fullOverReadWriteFieldValue = fullOverReadWriteField.GetValue(instance);
        Console.WriteLine($"The value of the full-over read-write field is {fullOverReadWriteFieldValue}.");

        fullOverReadWriteField.SetValue(instance, 1337);
        fullOverReadWriteFieldValue = fullOverReadWriteField.GetValue(instance);
        Console.WriteLine($"The value of the full-over read-write field is now {fullOverReadWriteFieldValue}.");

        var readWriteField = fields.First(fields => fields.Name == "_readWriteField");
        var readWriteFieldValue = readWriteField.GetValue(instance);
        Console.WriteLine($"The value of the read-write field is {readWriteFieldValue}.");

        // remember that thing we wanted to try out?
        readOnlyField.SetValue(instance, 456);
        readOnlyFieldValue = readOnlyField.GetValue(instance);
        Console.WriteLine($"The value of the read-only field is now {readOnlyFieldValue}.");
    }

    public sealed class OurClass
    {
        private static readonly string PrivateStaticField = "Hello, World!";

        private readonly int _readOnlyField = 123;
        private int _readWriteField;

        public int ReadWriteAutoProperty { get; set; }

        public int ReadOverReadWriteField => _readWriteField;

        public int FullOverReadWriteField
        {
            get => _readWriteField;
            set => _readWriteField = value;
        }
    }
}