using Carrigan.Core.Interfaces.IModels;

namespace Carrigan.Core.Extensions;

/// <summary>
/// Extension methods for <see cref="List{T}"/>
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Sorts <see cref="T"/> by T.Order, and returns the <see cref="T"/>
    /// <see cref="T"/> must implement the <see cref="IOrder"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the List</typeparam>
    /// <param name="theList">the <see cref="T"/> to be sorted</param>
    /// <returns>the <see cref="T"/> after being sorted, for chaining</returns>
    public static IEnumerable<T> SortByOrder<T>(this List<T> theList) where T : IOrder
    {
        IComparer<IOrder> comparer = Comparer<IOrder>.Create((x, y) => x.Order.CompareTo(y.Order));
        theList.Sort((IComparer<T>?)comparer);
        return theList.OrderBy(x => x.Order);
    }
}