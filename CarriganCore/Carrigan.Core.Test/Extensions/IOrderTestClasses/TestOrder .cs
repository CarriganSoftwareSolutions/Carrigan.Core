

using Carrigan.Core.Interfaces.IModels;

namespace Carrigan.Core.Test.Extensions.IOrderTestClasses;


// A simple implementation of IOrder for testing purposes.
public class TestOrder : IOrder
{
    public long Order { get; }

    public TestOrder(long order)
    {
        Order = order;
    }
}
