using Carrigan.Core.DataTypes;

namespace Carrigan.Core.Test.DataTypeTests.Examples;

public sealed class MemberNameIgnoreCase : StringWrapper
{
    public MemberNameIgnoreCase(string? value)
        : base(value, StringComparison.OrdinalIgnoreCase)
    {
    }
}
