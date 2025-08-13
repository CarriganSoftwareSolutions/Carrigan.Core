using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Carrigan.Core.Extensions;

public static class PropertyInfoExtensions
{
    public static string GetDisplayName(this PropertyInfo property)
    {
        // Check if the DisplayNameAttribute is applied
        string? displayName = property?.GetCustomAttribute<DisplayAttribute>()?.Name;

        // Fallback to the property name if no DisplayNameAttribute is found
        if (displayName.IsNullOrWhiteSpace())
        {
            displayName = property?.Name;
        }

        // Fallback to the property name if no DisplayNameAttribute is found
        return displayName ?? string.Empty;
    }
}
