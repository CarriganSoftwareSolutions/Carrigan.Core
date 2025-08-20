using System.Diagnostics.CodeAnalysis;

namespace Carrigan.Core.Extensions;

/// <summary>
/// Extension methods for <see cref="Guid"/>
/// </summary>
public static class GuidExtensions
{
#pragma warning disable IDE1006 // Naming Styles
    private static bool _IsNullOrEmpty(this Guid? guid) => (guid ?? Guid.Empty) == Guid.Empty;
#pragma warning restore IDE1006 // Naming Styles

    /// <summary>
    /// Returns true if the guid is null or empty
    /// </summary>
    /// <param name="guid"></param>
    /// <returns>Returns true if the guid is null or empty</returns>
    public static bool IsNullOrEmpty(this Guid guid) =>
        _IsNullOrEmpty(guid);

    /// <summary>
    /// Returns true if the guid is null or empty
    /// </summary>
    /// <param name="guid"></param>
    /// <returns>Returns true if the guid is null or empty</returns>
    public static bool IsNullOrEmpty(this Guid? guid) =>
        guid._IsNullOrEmpty();

    /// <summary>
    /// Returns false if the guid is null or empty
    /// </summary>
    /// <param name="guid"></param>
    /// <returns>Returns false if the guid is null or empty</returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this Guid guid) =>
        !_IsNullOrEmpty(guid);

    /// <summary>
    /// Returns false if the guid is null or empty
    /// </summary>
    /// <param name="guid"></param>
    /// <returns>Returns false if the guid is null or empty</returns>
    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this Guid? guid) =>
        !guid._IsNullOrEmpty();
}
