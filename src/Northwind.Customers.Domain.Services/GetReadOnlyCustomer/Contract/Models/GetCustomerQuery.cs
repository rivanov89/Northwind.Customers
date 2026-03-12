namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Contract.Models;

public class GetCustomerQuery 
{
    public readonly string CustomerId;

    public GetCustomerQuery(string customerId)
    {
        CustomerId = customerId;
    }
}