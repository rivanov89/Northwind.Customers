namespace Northwind.Customers.Web.Services.Customers
{
    public class CustomersApiOptions
    {
        public string BaseAddress { get; set; }
        public string GetCustomersTemplate { get; set; }
        public string GetCustomerTemplate { get; set; }
        public string GetCustomerOrdersTemplate { get; set; }
        public int ApiVersion { get; set; }
    }                                     
}
