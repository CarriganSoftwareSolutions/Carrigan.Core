using Carrigan.Core.Test.Extensions.IOrderTestClasses;
using Carrigan.Core.Extensions;

namespace Carrigan.Core.Test.Extensions;

public class ListExtensionsTests
{
    [Fact]
    public void SortByOrder_SortsAscending()
    {
        // Arrange: create a list of TestOrder objects in unsorted order.
        List<TestOrder> orders =
        [
            new TestOrder(5),
            new TestOrder(2),
            new TestOrder(8),
            new TestOrder(3)
        ];

        // Act: sort using the SortByOrder extension method.
        List<TestOrder> sortedOrders = [.. orders.SortByOrder()];

        // Assert: verify the list is sorted by the Order property.
        Assert.Equal(2, sortedOrders[0].Order);
        Assert.Equal(3, sortedOrders[1].Order);
        Assert.Equal(5, sortedOrders[2].Order);
        Assert.Equal(8, sortedOrders[3].Order);
        // Assert: verify the list is sorted by the Order property.
        Assert.Equal(2, orders[0].Order);
        Assert.Equal(3, orders[1].Order);
        Assert.Equal(5, orders[2].Order);
        Assert.Equal(8, orders[3].Order);
    }

    [Fact]
    public void SortByOrder_WithEmptyCollection_ReturnsEmpty()
    {
        // Arrange: an empty list.
        List<TestOrder> orders = [];

        // Act: apply the extension method.
        IEnumerable<TestOrder> sortedOrders = [.. orders.SortByOrder()];

        // Assert: the result should be an empty list.
        Assert.Empty(sortedOrders);
    }

    [Fact]
    public void SortByOrder_WithIdenticalOrderValues_MaintainsCount()
    {
        // Arrange: create a list where all items have the same Order value.
        List<TestOrder> orders =
        [
            new TestOrder(1),
            new TestOrder(1),
            new TestOrder(1)
        ];

        // Act: sort using the extension method.
        IEnumerable<TestOrder> sortedOrders = orders.SortByOrder();

        // Assert: the count remains unchanged and all orders are equal.
        Assert.Equal(3, sortedOrders.Count());
        Assert.All(sortedOrders, order => Assert.Equal(1, order.Order));
    }
}
