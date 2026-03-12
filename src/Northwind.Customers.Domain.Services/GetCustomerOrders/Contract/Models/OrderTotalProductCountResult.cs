using Northwind.Customers.Data.Models;
using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Domain.Services.GetCustomerOrders.Contract.Models
{
    public class OrderTotalProductCountResult: BasePagedQuery
    {
        public OrderTotalProductCount Order { get; }

        public OrderTotalProductCountResult(OrderTotalProductCount order, int pageIdx, int pageSize) : base(pageSize, pageIdx)
        {
            Order = order;
        }
    }
}
