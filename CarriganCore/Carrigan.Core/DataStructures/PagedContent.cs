using Carrigan.Core.Interfaces;

namespace Carrigan.Core.DataStructures;
/// <summary>
/// An concrete base class for paged data content base class, which implements the <see cref="IPagedContent{T}"/> interface.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedContent<TModel> : PagedContentBase<TModel>
{
    private IPageAccess<TModel> _access;
    private uint _PageSize;
    private uint _PageNumber;

    /// <summary>
    /// Concrete constructor for paged content implementation.
    /// </summary>
    /// <param name="access">Interface for asynchronous access to page-based data retrieval</param>
    /// <param name="pageNumber">Initial page number</param>
    /// <param name="pageSize">Page size</param>
    /// <exception cref="ArgumentNullException">Throws an <see cref="ArgumentNullException"> access is null.</exception>
    public PagedContent(IPageAccess<TModel> access, uint pageNumber, uint pageSize)
    {
        _access = access ?? throw new ArgumentNullException(nameof(access));
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
    /// <summary>
    /// Override implementation for abstract getter for the current page number and the protected setter.
    /// </summary>
    public override uint PageNumber
    {
        get => _PageNumber;
        protected set
        {
            _access.ClearCache();
            _PageNumber = value;
        }
    }
    /// <summary>
    /// Override implementation for the abstract getter for the page size and the protected setter.
    /// </summary>
    public override uint PageSize
    {
        get => _PageSize;
        protected set
        {
            _access.ClearCache();
            _PageSize = value;
        }
    }

    /// <summary>
    /// Override implementation for the abstract getter for count of the total number of items represented in the data structure.
    /// </summary>
    public override uint TotalItemCount =>
        (uint)_access.CountAll();

    /// <summary>
    /// Override implementation that returns a <see cref="IEnumerable{T}"/> that represents the underlying data.
    /// </summary>
    /// <returns>Returns a <see cref="IEnumerable{T}"/> that represents the underlying data.</returns>
    public override IEnumerable<TModel> AsEnumerable() =>
        _access.GetAll();

    /// <summary>
    /// Override implementation that returns the current page of data represented by an <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <returns>Returns the current page of data represented by an <see cref="IEnumerable{T}"/></returns>
    public override IEnumerable<TModel> PageAsEnumerable() =>
        _access.GetPage(PageNumber, PageSize);
}
