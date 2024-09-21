public sealed class AttributeBasicsExample
{
    public void Run()
    {
        // note how Visual Studio is warning us that
        // this type is obsolete?
        MyObsoleteType myObsoleteType = new();

        // note how Visual Studio is warning us that
        // this method is obsolete?
        myObsoleteType.TheOldMethod();
        myObsoleteType.TheNewFancyMethod();
    }

    [Serializable]
    public sealed class MySerializableType
    {
    }

    [Obsolete("This type is obsolete. Use MyNewType instead.")]
    public sealed class MyObsoleteType
    {
        public void TheNewFancyMethod()
        {
        }

        [Obsolete("This method is obsolete. Use TheNewFancyMethod instead.")]
        public void TheOldMethod()
        {
        }
    }

    // we can make our very own attributes!
    public sealed class SuperFancyAttribute : Attribute
    {
        public SuperFancyAttribute(
            string someString,
            int someInt,
            // NOTE: we can't create compile-time
            // attributes with non-constant values!
            object? someObject)
        {
            SomeString = someString;
            SomeInt = someInt;
            SomeObject = someObject;
        }

        public string SomeString { get; }

        public int SomeInt { get; }

        public object? SomeObject { get; set; }
    }

    // FIXME: this won't work because object is not constant!
    //[SuperFancy("Hello, World!", 42, new object())]
    public sealed class MySuperFancyType
    {
    }
}