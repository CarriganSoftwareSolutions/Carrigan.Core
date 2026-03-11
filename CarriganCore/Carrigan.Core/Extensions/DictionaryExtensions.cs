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
    public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, params IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs) where TKey : notnull
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        foreach (KeyValuePair<TKey, TValue> keyValuePair in keyValuePairs)
        {
            dictionary.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }


    public static bool DoesNotContainKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key) =>
        !dictionary.ContainsKey(key);
}
