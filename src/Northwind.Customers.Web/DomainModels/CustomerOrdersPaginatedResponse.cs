namespace Northwind.Customers.Web.DomainModels;

public class CustomerOrdersPaginatedResponse
{
    public int PageCount { get; set; }
    public List<OrderDto> Data { get; set; }
}