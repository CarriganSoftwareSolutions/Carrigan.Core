using System.Diagnostics.CodeAnalysis;

namespace Carrigan.Core.Extensions;

public static class GuidExtensions
{
#pragma warning disable IDE1006 // Naming Styles
    private static bool _IsNullOrEmpty(this Guid? guid) => (guid ?? Guid.Empty) == Guid.Empty;
#pragma warning restore IDE1006 // Naming Styles
    public static bool IsNullOrEmpty(this Guid guid) =>
        _IsNullOrEmpty(guid);

    public static bool IsNullOrEmpty(this Guid? guid) =>
        guid._IsNullOrEmpty();

    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this Guid guid) =>
        !_IsNullOrEmpty(guid);

    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this Guid? guid) =>
        !guid._IsNullOrEmpty();
}
