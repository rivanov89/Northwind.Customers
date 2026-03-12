namespace Northwind.Customers.Web.DomainModels;

public class OrderDto
{
    public decimal Total { get; set; }

    public int ProductCount { get; set; }

    public bool AnyProductDiscontinued { get; set; }

    public bool InStockIssue { get; set; }
}