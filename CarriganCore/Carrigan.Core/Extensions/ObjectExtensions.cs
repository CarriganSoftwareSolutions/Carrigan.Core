using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Carrigan.Core.Extensions;

/// <summary>
/// Extension methods for <see cref="object"/>
/// </summary>
public static class ObjectExtensions
{
    #region HasValue
    /// <summary>
    /// Determines whether the provided object has a meaningful value.
    /// For strings: not null, not empty, and not whitespace.
    /// For collections: not null and contains at least one element.
    /// For nullable value types: has a value.
    /// For other types: not null.
    /// </summary>
    /// <param name="obj">The object to evaluate.</param>
    /// <returns>True if the object has a value; otherwise, false.</returns>
    public static bool HasValue([NotNullWhen(true)] this object? obj)
    {
        if (obj == null)
            return false;

        // Handle string
        if (obj is string str)
            return !string.IsNullOrWhiteSpace(str);

        // Handle IEnumerable (but exclude string to avoid treating it as IEnumerable<char>)
        if (obj is IEnumerable enumerable && obj is not string)
        {
            // Check if the collection has at least one element
            //When I originally wrote this I had intended on it just having a value.
            //I did not consider that the value might be null.
            //After consideration, I decided that a list with a null value is a meaningful value?
            //However, this is a coin toss. Now that it is in use, changing it is risky.
            return enumerable.Cast<object>().Any();
        }

        // Handle Nullable<T>
        Type type = obj.GetType();
        if (IsNullable(type))
        {
            // Use cached reflection information for improved performance
            (PropertyInfo? HasValue, PropertyInfo? Value) cache = NullablePropertyCache.GetOrAdd(type, t =>
            {
                PropertyInfo? hasValueProp = t.GetProperty("HasValue");
                PropertyInfo? valueProp = t.GetProperty("Value");
                return (HasValue: hasValueProp, Value: valueProp);
            });

            if (cache.HasValue is not null)
            {
                bool? hasValue = (bool?)cache.HasValue.GetValue(obj);
                if (hasValue is not true)
                    return false;

                // If it has a value, further check if it's a string or IEnumerable
                return cache.Value?.GetValue(obj) is not null;
            }
        }

        // For other reference types, just check not null
        return true;
    }

    #endregion

    #region IsNullable
    /// <summary>
    /// Determines whether the specified type is a Nullable type.
    /// </summary>
    /// <param name="type">The type to evaluate.</param>
    /// <returns>True if the type is Nullable; otherwise, false.</returns>
    public static bool IsNullable(Type type) =>
        Nullable.GetUnderlyingType(type) != null;
    #endregion

    #region GetNullableValue
    /// <summary>
    /// Retrieves the value of a Nullable<T> type.
    /// </summary>
    /// <param name="nullable">The nullable object.</param>
    /// <returns>The underlying value if present; otherwise, null.</returns>
    public static object? GetNullableValue(object nullable)
    {
        Type type = nullable.GetType();
        PropertyInfo? valueProperty = type.GetProperty("Value");
        if (valueProperty != null)
        {
            return valueProperty.GetValue(nullable);
        }
        return null;
    }
    #endregion

    #region Reflection Cache
    // Cache for reflection information on nullable types to reduce overhead.
    private static readonly ConcurrentDictionary<Type, (PropertyInfo? HasValue, PropertyInfo? Value)> NullablePropertyCache = new();
    #endregion

    /// <summary>
    /// Returns the underlying type of the specified object instance.
    /// </summary>
    /// <remarks>
    /// Behavior:
    /// <list type="bullet">
    /// <item><description>If the instance is <c>null</c>, returns <c>null</c>.</description></item>
    /// <item><description><b>Nullable&lt;T&gt;</b>: returns <c>T</c>.</description></item>
    /// <item><description><b>Enum</b>: returns the enum's underlying integral type.</description></item>
    /// <item><description>Anything else: returns the actual runtime type.</description></item>
    /// </list>
    /// </remarks>
    /// <param name="instance">The object instance being examined.</param>
    /// <returns>
    /// A <see cref="Type"/> representing the underlying type of
    /// <paramref name="instance"/>, or <c>null</c> if the instance is <c>null</c>.
    /// </returns>
    public static Type? GetUnderlyingTypeOrNull(this object? instance)
    {
        if (instance is null)
        {
            return null;
        }

        Type type = instance.GetType();
        return type.GetUnderlyingType();
    }
}
