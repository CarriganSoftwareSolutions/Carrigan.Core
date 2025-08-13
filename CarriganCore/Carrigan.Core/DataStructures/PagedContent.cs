using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataStructures;

public class PagedContent<TModel> : PagedContentBase<TModel>
{
    private IPageAccess<TModel> _access;
    private uint _PageSize;
    private uint _PageNumber;

    public PagedContent(IPageAccess<TModel> access, uint pageNumber, uint pageSize)
    {
        _access = access ?? throw new ArgumentNullException(nameof(access));
        PageNumber = pageNumber;
        PageSize = pageSize;
    }


    public override uint PageNumber
    {
        get => _PageNumber;
        protected set
        {
            _access.ClearCache();
            _PageNumber = value;
        }
    }

    public override uint PageSize 
    { 
        get => _PageSize;
        protected set
        {
            _access.ClearCache();
            _PageSize = value;
        }
    }

    public override uint TotalItemCount =>
        (uint)_access.CountAll();

    public override IEnumerable<TModel> AsEnumerable() =>
        _access.GetAll();

    public override IEnumerable<TModel> PageAsEnumerable() =>
        _access.GetPage(PageNumber, PageSize);
}
