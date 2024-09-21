using System.Runtime.CompilerServices;

public sealed class UnsafeAccessorExample
{
    public class OurType
    {
        private static int _staticPrivateField = 123456;

        private int _privateField;

        private OurType(int i)
        {
            _privateField = i;
        }

        private static void StaticPrivateMethod()
        {
            Console.WriteLine($"Static private: {_staticPrivateField}");
        }

        private void PrivateMethod()
        {
            Console.WriteLine($"Instance private: {_privateField}");
        }

        private static int StaticPrivateProperty { get => _staticPrivateField; }

        private int PrivateProperty { get => _privateField; }
    }

    public void Run()
    {

        OurType instance = PrivateCtor(42);

        int privatePropertyValue = GetPrivateProperty(instance);
        Console.WriteLine($"Private property: {privatePropertyValue}");

        ref int referenceToPrivateField = ref GetSetPrivateField(instance);

        ref int referenceToStaticPrivateField = ref GetSetStaticPrivateField(null);
        Console.WriteLine($"Static private field: {referenceToStaticPrivateField}");

        PrivateMethod(instance);
        StaticPrivateMethod(null);

        referenceToPrivateField = 1337;
        PrivateMethod(instance);

        [UnsafeAccessor(UnsafeAccessorKind.Constructor)]
        extern static OurType PrivateCtor(
            int i);

        [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_privateField")]
        extern static int GetPrivateField(
            OurType instance);

        [UnsafeAccessor(UnsafeAccessorKind.StaticField, Name = "_staticPrivateField")]
        extern static ref int GetSetStaticPrivateField(
            OurType instance);

        [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "get_PrivateProperty")]
        extern static int GetPrivateProperty(
            OurType instance);

        [UnsafeAccessor(UnsafeAccessorKind.Method, Name = "PrivateMethod")]
        extern static void PrivateMethod(
            OurType instance);

        [UnsafeAccessor(UnsafeAccessorKind.StaticMethod, Name = "StaticPrivateMethod")]
        extern static void StaticPrivateMethod(
            OurType instance);

        [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_privateField")]
        extern static ref int GetSetPrivateField(
            OurType instance);
    }
}