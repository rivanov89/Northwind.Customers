namespace Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomersAdapter
{
    public interface IGetReadOnlyCustomersWebAdapter
    {
        Task<IResult> GetCustomersAsync(string? name, int pageIdx, int pageSize, CancellationToken token);
    }
}
