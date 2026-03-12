namespace Northwind.Customers.Web.DomainModels;

public class CustomerPaginatedResponse
{
    public int PageCount { get; set; }
    public List<CustomerDto> Data{ get; set; }
}