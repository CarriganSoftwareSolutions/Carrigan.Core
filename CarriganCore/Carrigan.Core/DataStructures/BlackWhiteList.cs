using Carrigan.Core.Extensions;

namespace Carrigan.Core.DataStructures;

/// <summary>
/// BlackWhiteList is a data structure that allows you to maintain a list of allowed and disallowed items.
/// It uses a dictionary to store the items and their allowed/disallowed status. 
/// The AddWhiteListValues and AddBlackListValues methods will throw an exception if you try to add an item that already exists in the opposite list. 
/// The IsAllowed method checks if an item is in the whitelist and not in the blacklist, returning true if it is allowed and false otherwise.
/// </summary>
/// <typeparam name="T"></typeparam>
public class BlackWhiteList<T> where T : notnull
{
    /// <summary>
    /// The WhiteBlackList dictionary stores the items and their allowed/disallowed status.
    /// </summary>
    private Dictionary<T, bool> WhiteBlackList { get; }

    /// <summary>
    /// The constructor initializes the WhiteBlackList dictionary.
    /// </summary>
    public BlackWhiteList() =>
        WhiteBlackList = [];

    /// <summary>
    /// The constructor initializes the WhiteBlackList dictionary with the provided white list and black list values.
    /// </summary>
    /// <param name="whiteListValues">
    /// The whiteListValues parameter is an IEnumerable of type T that represents the items that are allowed.
    /// </param>
    /// <param name="blackListValues">
    /// The blackListValues parameter is an optional IEnumerable of type T that represents the items that are disallowed.
    /// </param>
    public BlackWhiteList(IEnumerable<T> whiteListValues, IEnumerable<T>? blackListValues = null)
    {
        WhiteBlackList = [];
        AddWhiteListValues(whiteListValues);
        AddBlackListValues(blackListValues ?? []);
    }
    /// <summary>
    /// The AddWhiteListValues method adds the provided values to the whitelist.
    /// </summary>
    /// <param name="values">
    /// The values parameter is an IEnumerable of type T that represents the items to be added to the whitelist.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// The AddWhiteListValues method will throw an InvalidOperationException if you try to add an item that already exists in the blacklist.
    /// </exception>
    public void AddWhiteListValues(params IEnumerable<T> values) =>
        values.ForEach(item =>
        {
            if (WhiteBlackList.TryGetValue(item, out bool value) && !value)
                throw new InvalidOperationException($"The item {item} cannot be added to the whitelist because it already exists in the blacklist.");
            else
                WhiteBlackList[item] = true;
        });

    /// <summary>
    /// The AddBlackListValues method adds the provided values to the blacklist.
    /// </summary>
    /// <param name="values">
    /// The values parameter is an IEnumerable of type T that represents the items to be added to the blacklist.
    /// </param>
    /// <exception cref="InvalidOperationException">
    /// The AddBlackListValues method will throw an InvalidOperationException if you try to add an item that already exists in the whitelist.
    /// </exception>
    public void AddBlackListValues(params IEnumerable<T> values) => 
        values.ForEach( item=>
        {
            if(WhiteBlackList.TryGetValue(item, out bool value) && value)
                throw new InvalidOperationException($"The item { item } cannot be added to the blacklist because it already exists in the whitelist.");
            else
                WhiteBlackList[item] = false;
        });

    /// <summary>
    /// The IsAllowed method checks if the provided value is in the whitelist and not in the blacklist, returning true if it is allowed and false otherwise.
    /// </summary>
    /// <param name="value">
    /// The value parameter is of type T that represents the item to be checked for allowed/disallowed status.
    /// </param>
    /// <returns>
    /// The IsAllowed method returns a boolean value indicating whether the provided value is allowed (true) or disallowed (false) based on its presence in the whitelist and blacklist.
    /// </returns>
    public bool IsAllowed(T value) =>
        WhiteBlackList.ContainsKey(value) && WhiteBlackList[value];
}
