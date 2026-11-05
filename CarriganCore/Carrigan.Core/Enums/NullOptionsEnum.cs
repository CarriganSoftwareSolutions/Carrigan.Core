namespace Carrigan.Core.Enums;
/// <summary>
/// Specifies how <c>null</c> elements in an enumerable should be handled.
/// </summary>

public enum NullOptionsEnum
{
    Allowed,
    Exception,
    FilteredOut,
    [Obsolete("Use Exception instead.")]
    NullReferenceException,
    [Obsolete("Use ArgumentException instead.")]
    ArgumentNullException,
    ArgumentException
}
