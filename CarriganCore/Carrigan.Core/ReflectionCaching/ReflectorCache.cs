using System.Reflection;

namespace Carrigan.Core.ReflectionCaching;

public static class ReflectorCache<T>
{
    public readonly static Type Type = typeof(T);
    public static IEnumerable<PropertyInfo> ReadablePublicInstanceProperties => _LazyPublicInstanceProperties.Value.Where(property => property.CanRead);
    public static IEnumerable<PropertyInfo> WriteablePublicInstanceProperties => _LazyPublicInstanceProperties.Value.Where(property => property.CanWrite);
    

    private static readonly Lazy<IEnumerable<PropertyInfo>> _LazyPublicInstanceProperties;
    static ReflectorCache()
    {

        _LazyPublicInstanceProperties = new Lazy<IEnumerable<PropertyInfo>>
            (() =>
                Type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            );
    }

}
