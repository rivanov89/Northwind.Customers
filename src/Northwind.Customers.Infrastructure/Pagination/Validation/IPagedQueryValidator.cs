using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Infrastructure.Pagination.Validation;

public interface IPagedQueryValidator<TClient> : IValidator<BasePagedQuery>
{
}