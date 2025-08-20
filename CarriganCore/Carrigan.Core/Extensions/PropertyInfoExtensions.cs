using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Carrigan.Core.Extensions;

/// <summary>
/// Extension methods for <see cref="PropertyInfo"/>
/// </summary>
public static class PropertyInfoExtensions
{
    /// <summary>
    /// Get the <see cref="DisplayAttribute"/> of the <see cref="PropertyInfo"/>
    /// </summary>
    /// <param name="property">the property to get the display name of</param>
    /// <returns>Returns the name defined by <see cref="DisplayAttribute"/> of the <see cref="PropertyInfo"/></returns>
    public static string GetDisplayName(this PropertyInfo property)
    {
        // Check if the DisplayNameAttribute is applied
        string? displayName = property?.GetCustomAttribute<DisplayAttribute>()?.Name;

        // Fall back to the property name if no DisplayNameAttribute is found
        if (displayName.IsNullOrWhiteSpace())
        {
            displayName = property?.Name;
        }

        // Fallback to the property name if no DisplayNameAttribute is found
        return displayName ?? string.Empty;
    }
}
