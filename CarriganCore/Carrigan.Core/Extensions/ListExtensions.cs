using Carrigan.Core.Interfaces.IModels;

namespace Carrigan.Core.Extensions;

public static class ListExtensions
{
    public static IEnumerable<T> SortByOrder<T>(this List<T> source) where T : IOrder
    {
        IComparer<IOrder> comparer = Comparer<IOrder>.Create((x, y) => x.Order.CompareTo(y.Order));
        source.Sort((IComparer<T>?)comparer);
        return source.OrderBy(x => x.Order);
    }
}