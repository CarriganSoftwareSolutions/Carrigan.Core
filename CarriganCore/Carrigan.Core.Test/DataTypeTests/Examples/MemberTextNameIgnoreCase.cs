using Carrigan.Core.DataTypes;

namespace Carrigan.Core.Test.DataTypeTests.Examples;

public sealed class MemberTextNameIgnoreCase : StringWrapper
{
    public MemberTextNameIgnoreCase(string? value)
        : base(value, StringComparison.OrdinalIgnoreCase)
    {
    }
}
