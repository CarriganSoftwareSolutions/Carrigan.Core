using Carrigan.Core.Interfaces.IModels;
using System.Diagnostics.CodeAnalysis;

namespace Carrigan.Core.Extensions;

/// <summary>
/// Extension methods for the <see cref="IEmpty"/> interface
/// </summary>
public static class IEmptyExtensions
{
    /// <summary>
    /// return true if null or empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return true if null or empty</returns>
    public static bool IsNullOrEmpty(this IEmpty? value) =>
        value == null || value.IsEmpty();
    /// <summary>
    /// return false if empty
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return false if null or empty</returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this IEmpty? value) =>
        value.IsNullOrEmpty() == false;
}
