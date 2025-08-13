using System.Diagnostics.CodeAnalysis;

namespace Carrigan.Core.Extensions;

public static class IEnumerableExtensions
{
    #region DoesNotContain
    public static bool DoesNotContain<T>(this IEnumerable<T> enumerable, T value)
    {
        return enumerable.Contains(value) == false;
    }
    #endregion

    #region ForEach
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);

        foreach (T item in source)
        {
            action(item);
        }
    }
    #endregion

    #region EndsWith

    public static bool EndsWith<T>(this IEnumerable<T> value, IEnumerable<T> postfix)
        => value.Skip(value.Count() - postfix.Count()).SequenceEqual(postfix);
    #endregion

    #region StartsWith
    public static bool StartsWith<T>(this IEnumerable<T> value, IEnumerable<T> prefix)
        => value.Take(prefix.Count()).SequenceEqual(prefix);
    #endregion

    #region None
    public static bool None<T>(this IEnumerable<T> values) =>
        values.Any() == false;
    #endregion

    #region IsNullOrEmpty
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? values) =>
        values?.None() ?? true;
    #endregion

    #region IsNotNullOrEmpty
    public static bool IsNotNullOrEmpty<T>([NotNullWhen(true)] this IEnumerable<T>? values) =>
        values.IsNullOrEmpty() == false;
    #endregion

}
