namespace Northwind.Customers.Web.Api.WebAdapters.GetReadOnlyCustomerAdapter
{
    public interface IGetReadOnlyCustomerOrdersWebAdapter
    {
        Task<IResult> GetCustomerOrdersByIdAsync(string id, int pageIdx, int pageSize, CancellationToken token);
    }
}
