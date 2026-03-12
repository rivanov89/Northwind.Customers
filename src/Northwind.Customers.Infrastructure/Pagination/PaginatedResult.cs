namespace Northwind.Customers.Infrastructure.Pagination
{
    public class PaginatedResult<TData>(int totalDataCount, int pageSize, TData[] data)
    {
        public int PageCount { get; } = (int)Math.Ceiling((decimal)totalDataCount / pageSize);

        public IEnumerable<TData> Data { get; } = data;
    }
}
