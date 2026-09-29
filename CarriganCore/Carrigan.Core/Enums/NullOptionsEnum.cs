namespace Carrigan.Core.Enums;
/// <summary>
/// Specifies how <c>null</c> elements in an enumerable should be handled.
/// </summary>

public enum NullOptionsEnum
{
    Allowed,
    [Obsolete("Use NullReferenceException or ArgumentNullException instead.")]
    Exception,
    FilteredOut,
    NullReferenceException,
    ArgumentNullException
}
