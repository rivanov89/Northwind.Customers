namespace Northwind.Customers.Web.Models
{
    public class OrderViewModel
    {
        public decimal Total { get; set; }

        public int ProductCount { get; set; }

        public bool AnyProductDiscontinued { get; set; }

        public bool InStockIssue { get; set; }
    }
}
