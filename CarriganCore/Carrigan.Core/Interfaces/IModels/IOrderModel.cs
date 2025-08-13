namespace Carrigan.Core.Interfaces.IModels;
public interface IOrder
{
    long Order { get; }

    // Static property that returns an IComparer to sort IOrder objects by their Order property.
    public static IComparer<IOrder> OrderComparer { get; } =
        Comparer<IOrder>.Create((x, y) => x.Order.CompareTo(y.Order));
}
