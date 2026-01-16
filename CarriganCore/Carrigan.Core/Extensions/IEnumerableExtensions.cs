using Carrigan.Core.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Carrigan.Core.Extensions;

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/>
/// </summary>
public static class IEnumerableExtensions
{
    #region DoesNotContain
    /// <summary>
    /// returns the negation of the Contains method.
    /// This is a pointless method I wrote, because I couldn't DMJ's criticisms of my coding style out of my head.
    /// So now I feel I need to have a method like this instead of just saying !enumerable.Contains(value)
    /// </summary>
    /// <typeparam name="T">the type of the enumeration</typeparam>
    /// <param name="enumerable">the enumerable</param>
    /// <param name="value">the value being looked for</param>
    /// <returns></returns>
    public static bool DoesNotContain<T>(this IEnumerable<T> enumerable, T value)
    {
        return enumerable.Contains(value) == false;
    }
    #endregion

    #region ForEach
    /// <summary>
    /// allows you to use a foreach in a LINQ chain
    /// This is a pointless method I wrote, because I couldn't DMJ's criticisms of my coding style out of my head.
    /// So now I feel I need to have a method like this instead of just using a foreach loop.
    /// </summary>
    /// <typeparam name="T">the type of the enumeration</typeparam>
    /// <param name="source">the enumerable</param>
    /// <param name="actions">the curly brace block of code</param>
    /// <returns></returns>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> actions)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(actions);

        foreach (T item in source)
        {
            actions(item);
        }
    }
    #endregion

    #region EndsWith
    /// <summary>
    /// Determines if the enumerable ends with the elements in postfix.
    /// </summary>
    /// <typeparam name="T">the type of the enumeration</typeparam>
    /// <param name="enumerable">the enumerable</param>
    /// <param name="postfix">the values being searched for at the end of the enumerable</param>
    /// <returns>Returns true if the enumerable ends with the elements in postfix, else false<</returns>
    public static bool EndsWith<T>(this IEnumerable<T> enumerable, IEnumerable<T> postfix)
        => enumerable.Skip(enumerable.Count() - postfix.Count()).SequenceEqual(postfix);
    #endregion

    #region StartsWith
    /// <summary>
    /// Determines if the enumerable starts with the elements in postfix.
    /// </summary>
    /// <typeparam name="T">the type of the enumeration</typeparam>
    /// <param name="enumerable">the enumerable</param>
    /// <param name="postfix">the values being searched for at the start of the enumerable</param>
    /// <returns>Returns true if the enumerable starts with the elements in postfix, else false<</returns>
    public static bool StartsWith<T>(this IEnumerable<T> value, IEnumerable<T> prefix)
        => value.Take(prefix.Count()).SequenceEqual(prefix);
    #endregion

    /// <summary>
    /// Returns true if the enumerable is empty, else false
    /// </summary>
    /// <typeparam name="T">the type of the enumeration</typeparam>
    /// <param name="enumerable">the enumerable</param>
    /// <returns>returns true if the enumerable is empty, else false</returns>
    #region None
    public static bool None<T>(this IEnumerable<T> enumerable) =>
        enumerable.Any() == false;
    #endregion

    #region IsNullOrEmpty
    /// <summary>
    /// return true if null or empty
    /// </summary>
    /// <param name="enumerable"></param>
    /// <returns>return true if null or empty</returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? enumerable) =>
        enumerable?.None() ?? true;
    #endregion

    #region IsNotNullOrEmpty
    /// <summary>
    /// return false if null or empty, else true
    /// </summary>
    /// <param name="enumerable"></param>
    /// <returns>return false if null or empty, else true</returns>
    public static bool IsNotNullOrEmpty<T>([NotNullWhen(true)] this IEnumerable<T>? enumerable) =>
        enumerable.IsNullOrEmpty() == false;
    #endregion
    /// <summary>
/// Specifies how <c>null</c> elements in an enumerable should be handled.
/// </summary>
    public static IEnumerable<T> Materialize<T>(this IEnumerable<T> enumerable, NullOptionsEnum nullOptionsEnum)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        switch (nullOptionsEnum)
        {
            case NullOptionsEnum.Allowed:
                return [.. enumerable];
            case NullOptionsEnum.Exception:
                List<T> list = [];
                foreach (T element in enumerable)
                {
                    if (element is null)
                        throw new NullReferenceException($"{nameof(enumerable)} contains disallowed nulls");

                    list.Add(element);
                }
                return list;
            case NullOptionsEnum.FilteredOut:
                return [.. enumerable.Where(static element => element is not null)];
            default:
                throw new InvalidOperationException($"{nameof(nullOptionsEnum)} contains unsupported value.");
        }
    }
}
