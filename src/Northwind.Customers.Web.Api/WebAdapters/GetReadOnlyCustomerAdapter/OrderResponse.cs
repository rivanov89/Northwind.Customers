namespace Northwind.Customers.Web.Api.WebAdapters.GetReadOnlyCustomerAdapter
{
    public class OrderResponse
    {
        public decimal Total { get; set; }

        public int ProductCount { get; set; }

        public bool AnyProductDiscontinued { get; set; }

        public bool InStockIssue { get; set; }
    }
}
