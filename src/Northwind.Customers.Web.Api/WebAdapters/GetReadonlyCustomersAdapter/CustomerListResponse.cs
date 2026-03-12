namespace Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomersAdapter;

public class CustomerListResponse
{
    public string CustomerId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int TotalOrderCount { get; set; }
}