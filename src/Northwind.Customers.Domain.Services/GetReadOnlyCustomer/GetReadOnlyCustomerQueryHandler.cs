using Microsoft.EntityFrameworkCore;
using Northwind.Customers.Data.EntityFrameworkCore;
using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using OneOf;

namespace Northwind.Customers.Domain.Services.GetReadOnlyCustomer
{
    public class GetReadOnlyCustomerQueryHandler : IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>>
    {
        private readonly NorthwindContext _database;

        public GetReadOnlyCustomerQueryHandler(NorthwindContext database)
        {
            _database = database;
        }

        public async Task<OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> ExecuteAsync(GetCustomerQuery query, CancellationToken token)
        {
            var queryable = _database.Customers.AsNoTracking();
            return await queryable.FirstOrDefaultAsync(c => c.CustomerId == query.CustomerId, token);
        }
    }
}
