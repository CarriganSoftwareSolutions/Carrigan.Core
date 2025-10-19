using System.Reflection;

namespace Carrigan.Core.ReflectionCaching;

/// <summary>
/// Provides a simple static caching mechanism for frequently used reflection metadata.
/// This class is primarily intended for use by <c>Carrigan.SqlTools</c> and other related libraries.
/// </summary>
/// <typeparam name="T">The data type being reflected.</typeparam>
public static class ReflectorCache<T>
{
    /// <summary>
    /// The reflected type. This field is static and read-only.
    /// </summary>
    public readonly static Type Type = typeof(T);
    /// <summary>
    /// Gets all public instance properties with accessible getters.
    /// Uses lazy initialization for improved performance.
    /// </summary>
    public static IEnumerable<PropertyInfo> ReadablePublicInstanceProperties => _LazyPublicInstanceProperties.Value.Where(property => property.CanRead);
    /// <summary>
    /// Gets all public instance properties with accessible setters.
    /// Uses lazy initialization for improved performance.
    /// </summary>
    public static IEnumerable<PropertyInfo> WriteablePublicInstanceProperties => _LazyPublicInstanceProperties.Value.Where(property => property.CanWrite);

    /// <summary>
    /// Lazily initialized collection of all public instance properties.
    /// </summary>
    private static readonly Lazy<IEnumerable<PropertyInfo>> _LazyPublicInstanceProperties;

    /// <summary>
    /// Static constructor that initializes the lazy property cache.
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
