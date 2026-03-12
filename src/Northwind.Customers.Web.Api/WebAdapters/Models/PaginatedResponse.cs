namespace Northwind.Customers.Web.Api.WebAdapters.Models
{
    public class PaginatedResponse<TData>(int pageCount, TData[] data)
    {
        public int PageCount { get; } = pageCount;

        public IEnumerable<TData> Data { get; } = data;
    }
}
