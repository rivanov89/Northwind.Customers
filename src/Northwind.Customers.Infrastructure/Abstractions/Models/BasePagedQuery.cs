namespace Northwind.Customers.Infrastructure.Abstractions.Models;

public abstract class BasePagedQuery
{
    protected BasePagedQuery(int pageSize, int pageIdx)
    {
        PageSize = pageSize;
        PageIdx = pageIdx;
    }

    public int PageIdx { get; }

    public int PageSize { get; }
}