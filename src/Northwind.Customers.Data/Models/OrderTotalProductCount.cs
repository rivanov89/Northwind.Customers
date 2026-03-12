namespace Northwind.Customers.Data.Models;

public class OrderTotalProductCount
{
    public string? CustomerId { get; set; }

    public decimal Total { get; set; }

    public int ProductCount { get; set; }

    public bool AnyProductDiscontinued { get; set; }

    public bool InStockIssue { get; set; }
}