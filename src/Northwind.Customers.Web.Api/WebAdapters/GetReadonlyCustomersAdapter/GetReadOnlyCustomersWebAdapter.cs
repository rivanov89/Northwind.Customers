using Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Contract.Models;
using Northwind.Customers.Infrastructure.Abstractions;
using Northwind.Customers.Infrastructure.ErrorHandling;
using Northwind.Customers.Web.Api.WebAdapters.Models;
using OneOf;

namespace Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomersAdapter
{
    public class GetReadOnlyCustomersWebAdapter : IGetReadOnlyCustomersWebAdapter
    {
        private readonly IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult, GeneralErrorResult>> _queryHandler;

        public GetReadOnlyCustomersWebAdapter(IQuery<GetCustomersQuery, OneOf<CustomersResult, GetCustomersInputValidationResult, GetCustomersOutputValidationResult, GeneralErrorResult>> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        public async Task<IResult> GetCustomersAsync(string? name, int pageIdx, int pageSize, CancellationToken token)
        {
            var customersResult = await _queryHandler.ExecuteAsync(new GetCustomersQuery(pageSize, pageIdx, name), token);
            return
                customersResult.Match(
                    customers => Results.Ok(new PaginatedResponse<CustomerListResponse>(customers.PageCount,
                        customers
                            .Data
                            .Select(x => new CustomerListResponse
                            {
                                CustomerId = x.CustomerId,
                                Name = x.Name,
                                TotalOrderCount = x.OrderCount
                            }).ToArray())),
                    Results.BadRequest,
                    outputValidationResult => Results.NotFound(outputValidationResult.ValidationFailureMessages),
                    _ => Results.InternalServerError()
                );
        }
    }
}
