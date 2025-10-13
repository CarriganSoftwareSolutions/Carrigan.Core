using Carrigan.Core.DataTypes;
using Carrigan.Core.Extensions;
using Carrigan.Core.Interfaces;

namespace Carrigan.Core.Test.DataTypeTests.Examples;

//This is included here to unit test the StringWarapper base class.

internal class MemberName : StringWrapper
{
    internal MemberName(string? name) : base(name) { }

    internal static MemberName? New(string? name)
    {
        if (name.IsNullOrEmpty())
            return null;
        else
            return new MemberName(name);
    }
}