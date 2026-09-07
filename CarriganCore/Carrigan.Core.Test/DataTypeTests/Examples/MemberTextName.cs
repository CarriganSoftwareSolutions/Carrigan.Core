using Carrigan.Core.DataTypes;
using Carrigan.Core.Extensions;
using Carrigan.Core.Interfaces;

namespace Carrigan.Core.Test.DataTypeTests.Examples;

//This is included here to unit test the TextWrapper base class.

internal class MemberTextName : TextWrapper
{
    internal MemberTextName(string? name) : base(name) { }

    internal static MemberTextName? New(string? name)
    {
        if (name.IsNullOrEmpty())
            return null;
        else
            return new MemberTextName(name);
    }
}