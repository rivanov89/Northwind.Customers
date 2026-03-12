using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models
{
    public class GetCustomerOrdersQuery : BasePagedQuery
    {
        public string CustomerId { get; }

        public GetCustomerOrdersQuery(int pageSize, int pageIdx, string customerId) : base(pageSize, pageIdx)
        {
            CustomerId = customerId;
        }
    }
}
