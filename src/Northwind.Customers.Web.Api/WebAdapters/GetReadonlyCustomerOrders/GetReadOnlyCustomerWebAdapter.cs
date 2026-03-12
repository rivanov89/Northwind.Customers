using Northwind.Customers.Data.Models;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using OneOf;

namespace Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomerOrders
{
    public class GetReadOnlyCustomerWebAdapter : IGetReadOnlyCustomerWebAdapter
    {
        private readonly IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> _queryHandler;

        public GetReadOnlyCustomerWebAdapter(IQuery<GetCustomerQuery, OneOf<Customer, GetCustomerInputValidationResult, GetCustomerOutputValidationResult, GeneralErrorResult>> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public async Task<IResult> GetCustomerByIdAsync(string id, CancellationToken token)
        {
            var customerResult = await _queryHandler.ExecuteAsync(new GetCustomerQuery(id), token);
            return customerResult.Match(
                customer => Results.Ok(new CustomerResponse
                {
                    Address = customer.Address,
                    City = customer.City,
                    CompanyName = customer.CompanyName,
                    ContactName = customer.ContactName,
                    ContactTitle = customer.ContactTitle,
                    Country = customer.Country,
                    CustomerId = customer.CustomerId,
                    Fax = customer.Fax,
                    Phone = customer.Phone,
                    PostalCode = customer.PostalCode,
                    Region = customer.Region
                }),
                Results.BadRequest,
                Results.NotFound,
                _ => Results.InternalServerError());
        }
    }
}
