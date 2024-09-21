namespace FindingTypes.ExtraTypes;

public sealed class PublicClassWithNoMembers
{
}

internal sealed class InternalClassWithNoMembers
{
}

public sealed class PublicClassWithNestedClass
{
    public sealed class NestedPublicClass
    {
    }

    internal sealed class NestedInternalClass
    {
    }

    private sealed class NestedPrivateClass
    {
    }
}

internal sealed class InternalClassWithNestedClass
{
    public sealed class NestedPublicClass
    {
    }

    internal sealed class NestedInternalClass
    {
    }

    private sealed class NestedPrivateClass
    {
    }
}