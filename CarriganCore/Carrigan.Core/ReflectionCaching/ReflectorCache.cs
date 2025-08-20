using System.Reflection;

namespace Carrigan.Core.ReflectionCaching;

/// <summary>
/// Simple static caching mechanism for frequently used reflections.
/// This is needed primarily for the Carrigan.SqlTOols, and other libraries
/// </summary>
/// <typeparam name="T">the data type your are reflecting</typeparam>
public static class ReflectorCache<T>
{
    /// <summary>
    /// The type, readonly static
    /// </summary>
    public readonly static Type Type = typeof(T);
    /// <summary>
    /// Public static getter for all public instance CanRead properties, using LazyLoad
    /// </summary>
    public static IEnumerable<PropertyInfo> ReadablePublicInstanceProperties => _LazyPublicInstanceProperties.Value.Where(property => property.CanRead);
    /// <summary>
    /// Public static getter for all public instance CanWrite properties, using LazyLoad
    /// </summary>
    public static IEnumerable<PropertyInfo> WriteablePublicInstanceProperties => _LazyPublicInstanceProperties.Value.Where(property => property.CanWrite);

    /// <summary>
    /// Public static getter for all public instance CanWrite properties, using LazyLoad
    /// </summary>
    private static readonly Lazy<IEnumerable<PropertyInfo>> _LazyPublicInstanceProperties;

    /// <summary>
    /// Static constructor
    /// </summary>
    static ReflectorCache()
    {

        _LazyPublicInstanceProperties = new Lazy<IEnumerable<PropertyInfo>>
            (() =>
                Type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            );
    }

}
