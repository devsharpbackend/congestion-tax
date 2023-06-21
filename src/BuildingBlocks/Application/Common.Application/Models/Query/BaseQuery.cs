
namespace Fintranet.BuildingBlocks.Common.Application.Models.Query;

public class BaseQuery : IRequest<Unit> { }
public class BaseQuery<T> : IRequest<T>
{
}
public class PagingQuery<T> : BaseQuery<T>
{
    public PagingQuery()
    {
        PageCount = 1000;
        _pageIndex = 1;
    }

    private int _pageIndex;
    public int PageCount { get; set; }
    public int PageIndex
    {
        get => PageCount * (_pageIndex - 1);
        set
        {
            if (value <= 0)
                _pageIndex = 1;

          _pageIndex = value;
        }
    }
}
