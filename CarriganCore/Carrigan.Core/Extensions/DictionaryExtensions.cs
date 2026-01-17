namespace Carrigan.Core.Extensions;

/// <summary>
/// Dictionary extension methods.
/// </summary>
public static class DictionaryExtensions
{
    /// <summary>
    /// Add all <see cref="KeyValuePair{TKey, TValue}"/> in <see cref="IEnumerable{KeyValuePair{TKey, TValue}}"/> to the <see cref="Dictionary{TKey, TValue}"/>
    /// </summary>
    /// <typeparam name="TKey">Dictionary Key type</typeparam>
    /// <typeparam name="TValue">Dictionary Value Type</typeparam>
    /// <param name="dictionary">The dictionary to add the key value pairs to.</param>
    /// <param name="keyValuePairs">key value pairs</param>
    public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, params IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs) where TKey : notnull, IEquatable<TKey>
    {
        foreach (KeyValuePair<TKey, TValue> keyValuePair in keyValuePairs)
        {
            dictionary.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }

    /// <summary>
    /// Adds a <see cref="KeyValuePair{TKey, TValue}"/> to the specified <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    /// <remarks>
    /// This extension method exists primarily to support C# collection expressions (for example, <c>[.. items]</c>)
    /// when the target type is <see cref="Dictionary{TKey, TValue}"/> and the source elements are
    /// <see cref="KeyValuePair{TKey, TValue}"/> instances.
    /// <para>
    /// The C# compiler requires the target collection type to expose an <c>Add</c> method that can be called
    /// with a single argument matching the element type. <see cref="Dictionary{TKey, TValue}"/> does not expose
    /// a public <c>Add(KeyValuePair&lt;TKey, TValue&gt;)</c> overload (it is only available through an explicit
    /// interface implementation), so this extension fills that gap.
    /// </para>
    /// <para>
    /// This method delegates to <see cref="Dictionary{TKey, TValue}.Add(TKey, TValue)"/> and therefore preserves
    /// the same behavior (for example, throwing when a duplicate key is added).
    /// </para>
    /// </remarks>
    /// <typeparam name="TKey">The dictionary key type.</typeparam>
    /// <typeparam name="TValue">The dictionary value type.</typeparam>
    /// <param name="dictionary">The dictionary to add the key–value pair to.</param>
    /// <param name="item">The key–value pair to add.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="dictionary"/> is <c>null</c>.</exception>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="item"/> contains a key that already exists in <paramref name="dictionary"/>.
    /// </exception>
    public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> item) where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        dictionary.Add(item.Key, item.Value);
    }
}
