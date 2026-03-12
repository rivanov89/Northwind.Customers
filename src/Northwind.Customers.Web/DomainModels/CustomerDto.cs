namespace Northwind.Customers.Web.DomainModels;

public class CustomerDto
{
    public string CustomerId { get; set; }
    public string Name { get; set; }
    public int TotalOrderCount { get; set; }
}