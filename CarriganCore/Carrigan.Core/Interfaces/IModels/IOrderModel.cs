namespace Carrigan.Core.Interfaces.IModels;

/// <summary>
/// Interface IOrder, this used as a prerequisite for using <see cref="Carrigan.Core.Extensions.ListExtensions.SortByOrder{T}(List{T})"/>
/// This is what allows the SortByOrder extension method to do its thing. 
/// This was part of a quick hack to sort by date with several models without having to implement all the .Net interfaces normally used for sorting.
/// This will likely be of little interest to anyone besides myself.
/// </summary>
public interface IOrder
{
    long Order { get; }

    // Static property that returns an IComparer to sort IOrder objects by their Order property.
    public static IComparer<IOrder> OrderComparer { get; } =
        Comparer<IOrder>.Create((x, y) => x.Order.CompareTo(y.Order));
}
