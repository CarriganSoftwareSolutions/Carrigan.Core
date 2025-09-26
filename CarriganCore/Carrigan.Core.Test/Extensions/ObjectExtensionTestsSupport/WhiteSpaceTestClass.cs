using Carrigan.Core.Interfaces;
using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions.ObjectExtensionTestsSupport;


/// <summary>
/// A custom nullable type for testing purposes.
/// </summary>
internal class WhiteSpaceTestClass : IWhiteSpace
{
    internal string Value { get; set; }

    public WhiteSpaceTestClass(string value) => 
        Value = value;

    public bool IsEmpty() => 
        Value.IsEmpty();
    public bool IsNotEmpty() =>
        Value.IsEmpty() is false;
    public bool IsWhiteSpace() => 
        Value.IsWhiteSpace();
    public bool IsNotWhiteSpace() =>
        Value.IsWhiteSpace() is false;
}
