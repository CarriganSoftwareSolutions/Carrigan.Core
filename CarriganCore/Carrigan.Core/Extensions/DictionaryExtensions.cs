namespace Carrigan.Core.Extensions;

public static class DictionaryExtensions
{
    public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, params IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs) where TKey : notnull, IEquatable<TKey>
    {
        foreach (KeyValuePair<TKey, TValue> keyValuePair in keyValuePairs)
        {
            dictionary.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
