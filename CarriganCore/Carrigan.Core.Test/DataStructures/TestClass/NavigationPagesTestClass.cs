using Carrigan.Core.DataStructures;
namespace Carrigan.Core.Test.DataStructures.TestClass;

public class NavigationPagesTestClass : PagedContentBase<int>
{
    public override uint PageNumber { get; protected set; }

    public override uint PageCount { get; }

    public NavigationPagesTestClass(uint pageNumber, uint pageCount)
    {
        PageNumber = pageNumber;
        PageCount = pageCount;
    }

    public override uint TotalItemCount => throw new NotImplementedException();

    public override IEnumerable<int> AsEnumerable()
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<int> PageAsEnumerable()
    {
        throw new NotImplementedException();
    }
}
