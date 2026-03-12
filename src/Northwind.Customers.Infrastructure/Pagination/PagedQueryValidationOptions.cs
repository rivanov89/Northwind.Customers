namespace Northwind.Customers.Infrastructure.Pagination;

public class PagedQueryValidationOptions<TClient>
{
    public int MaxPageSize { get; set; }
}