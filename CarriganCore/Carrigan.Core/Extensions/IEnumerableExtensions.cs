using Carrigan.Core.Enums;
using Carrigan.Core.Exceptions;
using Carrigan.Core.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

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
    public static bool DoesNotContain<T>(this IEnumerable<T> enumerable, T value) =>
        enumerable.Contains(value) == false;
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
    public static bool IsNullOrEmpty<T>([NotNullWhen(false)]this IEnumerable<T>? enumerable) =>
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

    #region Materialize
    /// <summary>
    /// Specifies how <c>null</c> elements in an enumerable should be handled.
    /// </summary>
    public static IEnumerable<T> Materialize<T>(this IEnumerable<T> enumerable, NullOptionsEnum nullOptionsEnum, [CallerArgumentExpression(nameof(enumerable))] string? variableName = null)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        string name = variableName ?? nameof(enumerable);
        List<T> list;
        switch (nullOptionsEnum)
        {
            case NullOptionsEnum.Allowed:
                return [.. enumerable];
#pragma warning disable CS0618 // Type or member is obsolete
            case NullOptionsEnum.NullReferenceException:
#pragma warning restore CS0618 // Type or member is obsolete
            case NullOptionsEnum.Exception:
                list = [];
                foreach (T element in enumerable)
                {
                    if (element is null)
                        throw new NullReferenceException($"{name} contains disallowed nulls");

                    list.Add(element);
                }
                return list;
#pragma warning disable CS0618 // Type or member is obsolete
            case NullOptionsEnum.ArgumentNullException:
#pragma warning restore CS0618 // Type or member is obsolete
            case NullOptionsEnum.ArgumentException:
                list = [];
                foreach (T element in enumerable)
                {
                    if (element is null)
                        throw new ArgumentNullException(name, "Collection contains disallowed null elements.");

                    list.Add(element);
                }
                return list;
            case NullOptionsEnum.FilteredOut:
                return [.. enumerable.Where(static element => element is not null)];

            default:
                throw new InvalidOperationException($"{nameof(nullOptionsEnum)} contains unsupported value.");
        }
    }

    /// <summary>
    /// Specifies how <c>null</c> or empty elements in an enumerable should be handled.
    /// </summary>
    public static IEnumerable<T> Materialize<T>(this IEnumerable<T> enumerable, NullOrEmptyOptionsEnum nullOptionsEnum, [CallerArgumentExpression(nameof(enumerable))] string? variableName = null) where T : IEmpty
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        string name = variableName ?? nameof(enumerable);
        List<T> list;
        switch (nullOptionsEnum)
        {
            case NullOrEmptyOptionsEnum.Allowed:
                return [.. enumerable];
            case NullOrEmptyOptionsEnum.Exception:
                list = [];
                foreach (T element in enumerable)
                {
                    if (element.IsNullOrEmpty())
                        throw new NullOrEmptyException(name, "contains disallowed null or empty elements.");

                    list.Add(element);
                }
                return list;
            case NullOrEmptyOptionsEnum.ArgumentException:
                list = [];
                foreach (T element in enumerable)
                {
                    if (element.IsNullOrEmpty())
                        throw new ArgumentNullOrEmptyException("Collection contains disallowed null or empty elements.", name);

                    list.Add(element);
                }
                return list;
            case NullOrEmptyOptionsEnum.FilteredOut:
                return [.. enumerable.Where(static element => element.IsNotNullOrEmpty())];

            default:
                throw new InvalidOperationException($"{nameof(nullOptionsEnum)} contains unsupported value.");
        }
    }

    /// <summary>
    /// Specifies how <c>null</c> or empty elements in an enumerable should be handled.
    /// </summary>
    public static IEnumerable<string> Materialize(this IEnumerable<string> enumerable, NullOrEmptyOptionsEnum nullOptionsEnum, [CallerArgumentExpression(nameof(enumerable))] string? variableName = null)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        string name = variableName ?? nameof(enumerable);
        List<string> list;
        switch (nullOptionsEnum)
        {
            case NullOrEmptyOptionsEnum.Allowed:
                return [.. enumerable];
            case NullOrEmptyOptionsEnum.Exception:
                list = [];
                foreach (string element in enumerable)
                {
                    if (element.IsNullOrEmpty())
                        throw new NullOrEmptyException(name, "contains disallowed null or empty elements.");

                    list.Add(element);
                }
                return list;
            case NullOrEmptyOptionsEnum.ArgumentException:
                list = [];
                foreach (string element in enumerable)
                {
                    if (element.IsNullOrEmpty())
                        throw new ArgumentNullOrEmptyException("Collection contains disallowed null or empty elements.", name);

                    list.Add(element);
                }
                return list;
            case NullOrEmptyOptionsEnum.FilteredOut:
                return [.. enumerable.Where(static element => element.IsNotNullOrEmpty())];

            default:
                throw new InvalidOperationException($"{nameof(nullOptionsEnum)} contains unsupported value.");
        }
    }


    /// <summary>
    /// Specifies how <c>null</c> or whitespace elements in an enumerable should be handled.
    /// </summary>
    public static IEnumerable<T> Materialize<T>(this IEnumerable<T> enumerable, NullOrWhiteSpaceOptionsEnum nullOptionsEnum, [CallerArgumentExpression(nameof(enumerable))] string? variableName = null) where T : IWhiteSpace
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        string name = variableName ?? nameof(enumerable);
        List<T> list;
        switch (nullOptionsEnum)
        {
            case NullOrWhiteSpaceOptionsEnum.Allowed:
                return [.. enumerable];
            case NullOrWhiteSpaceOptionsEnum.Exception:
                list = [];
                foreach (T element in enumerable)
                {
                    if (element.IsNullOrWhiteSpace())
                        throw new NullOrWhiteSpaceException(name, "contains disallowed null or white space elements.");

                    list.Add(element);
                }
                return list;
            case NullOrWhiteSpaceOptionsEnum.ArgumentException:
                list = [];
                foreach (T element in enumerable)
                {
                    if (element.IsNullOrWhiteSpace())
                        throw new ArgumentNullOrWhiteSpaceException("Collection contains disallowed null or white space elements.", name);

                    list.Add(element);
                }
                return list;
            case NullOrWhiteSpaceOptionsEnum.FilteredOut:
                return [.. enumerable.Where(static element => element.IsNotNullOrWhiteSpace())];

            default:
                throw new InvalidOperationException($"{nameof(nullOptionsEnum)} contains unsupported value.");
        }
    }

    /// <summary>
    /// Specifies how <c>null</c> or whitespace elements in an enumerable should be handled.
    /// </summary>
    public static IEnumerable<string> Materialize(this IEnumerable<string> enumerable, NullOrWhiteSpaceOptionsEnum nullOptionsEnum, [CallerArgumentExpression(nameof(enumerable))] string? variableName = null)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        string name = variableName ?? nameof(enumerable);
        List<string> list;
        switch (nullOptionsEnum)
        {
            case NullOrWhiteSpaceOptionsEnum.Allowed:
                return [.. enumerable];
            case NullOrWhiteSpaceOptionsEnum.Exception:
                list = [];
                foreach (string element in enumerable)
                {
                    if (element.IsNullOrWhiteSpace())
                        throw new NullOrWhiteSpaceException(name, "contains disallowed null or white space elements.");

                    list.Add(element);
                }
                return list;
            case NullOrWhiteSpaceOptionsEnum.ArgumentException:
                list = [];
                foreach (string element in enumerable)
                {
                    if (element.IsNullOrWhiteSpace())
                        throw new ArgumentNullOrWhiteSpaceException("Collection contains disallowed null or white space elements.", name);

                    list.Add(element);
                }
                return list;
            case NullOrWhiteSpaceOptionsEnum.FilteredOut:
                return [.. enumerable.Where(static element => element.IsNotNullOrWhiteSpace())];

            default:
                throw new InvalidOperationException($"{nameof(nullOptionsEnum)} contains unsupported value.");
        }
    }
    #endregion

    /// <summary>
    /// Returns an enumerable that is the same as the input enumerable, except the element at the specified index is skipped.
    /// </summary>
    /// <typeparam name="T">
    /// the type of the enumeration
    /// </typeparam>
    /// <param name="enumerable">the enumerable</param>
    /// <param name="index">the index of the element to skip</param>
    /// <returns>an enumerable with the element at the specified index skipped</returns>
    public static IEnumerable<T> SkipAt<T>(this IEnumerable<T> enumerable, int index)
    {
        ArgumentNullException.ThrowIfNull(enumerable);
        ArgumentOutOfRangeException.ThrowIfLessThan(index, 0, nameof(index));

        return enumerable.Where((_, i) => i != index);
    }

    /// <summary>
    /// Returns true if all elements in the enumerable are equal, false if they are not, and null if the enumerable is empty.
    /// </summary>
    /// <typeparam name="T">
    /// the type of the enumeration
    /// </typeparam>
    /// <param name="values">
    /// the enumerable
    /// </param>
    /// <returns>
    /// Returns true if all elements in the enumerable are equal, false if they are not, and null if the enumerable is empty.
    /// </returns>
    public static bool? AllEqual<T>(this IEnumerable<T> values)
    {
        ArgumentNullException.ThrowIfNull(values);

        using IEnumerator<T> enumerator = values.GetEnumerator();

        if (!enumerator.MoveNext())
            return null;

        T first = enumerator.Current;
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        while (enumerator.MoveNext())
            if (!comparer.Equals(enumerator.Current, first))
                return false;

        return true;
    }
}

