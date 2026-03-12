namespace Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomerOrders
{
    public interface IGetReadOnlyCustomerWebAdapter
    {
        Task<IResult> GetCustomerByIdAsync(string id, CancellationToken token);
    }
}
