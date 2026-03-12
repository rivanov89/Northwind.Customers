using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models;

public class OrderTotalProductCountOutputValidationResult : BaseValidationResult
{
    public OrderTotalProductCountOutputValidationResult(string[] validationFailureMessages) : base(validationFailureMessages)
    {
    }
}