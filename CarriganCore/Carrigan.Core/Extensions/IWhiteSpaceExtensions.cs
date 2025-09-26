using Carrigan.Core.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Carrigan.Core.Extensions;

/// <summary>
/// Extension methods for the <see cref="IWhiteSpaceExtensions"/> interface
/// </summary>
public static class IWhiteSpaceExtensions
{
    /// <summary>
    /// return true if null or whitespace
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return true if null or whitespace</returns>
    public static bool IsNullOrWhiteSpace(this IWhiteSpace? value) =>
        value == null || value.IsWhiteSpace();

    /// <summary>
    /// return false if null or whitespace
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return false if null or whitespace</returns>
    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this IWhiteSpace? value) =>
        value.IsNullOrWhiteSpace() is false;
}
