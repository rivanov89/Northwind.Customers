using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Northwind.Customers.Web.Api.WebAdapters.GetReadOnlyCustomerAdapter;
using Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomerOrders;
using Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomersAdapter;

namespace Northwind.Customers.Web.Api.API
{
    public static class CustomersAPI
    {
        public static WebApplication MapCustomersApi(this WebApplication app)
        {
            var versionSet = app.NewApiVersionSet()
                .HasApiVersion(new ApiVersion(1, 0))
                .ReportApiVersions()
                .Build();

            var groupBuilder = app.MapGroup("api/v{version:apiVersion}/Customers")
                .WithApiVersionSet(versionSet)
                .MapToApiVersion(1, 0);

            groupBuilder
                .MapGet("/", async ([FromQuery] string? name,
                    [FromQuery] int pageIdx, 
                    [FromQuery] int pageSize,
                    [FromServices] IGetReadOnlyCustomersWebAdapter webAdapter, 
                    CancellationToken token) => await webAdapter.GetCustomersAsync(name, pageIdx, pageSize, token));

            groupBuilder
                .MapGet("/{id}", async (string id,
                    [FromServices] ILogger<Program> logger,
                    [FromServices] IGetReadOnlyCustomerWebAdapter webAdapter,
                    CancellationToken token) => await webAdapter.GetCustomerByIdAsync(id, token));
            groupBuilder
                .MapGet("/{id}/Orders", async (string id, [FromQuery] int pageIdx, [FromQuery] int pageSize,
                    [FromServices] IGetReadOnlyCustomerOrdersWebAdapter webAdapter,
                    CancellationToken token) => await webAdapter.GetCustomerOrdersByIdAsync(id, pageIdx, pageSize, token));

            return  app;
        }
    }
}
