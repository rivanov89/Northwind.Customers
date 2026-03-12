using Asp.Versioning;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Northwind.Customers.Data.EntityFrameworkCore.Configuration;
using Northwind.Customers.Domain.Services.GetCustomerOrders.Configuration;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomer.Configuration;
using Northwind.Customers.Domain.Services.GetReadOnlyCustomers.Configuration;
using Northwind.Customers.Web.Api.API;
using Northwind.Customers.Web.Api.WebAdapters.GetReadOnlyCustomerAdapter;
using Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomerOrders;
using Northwind.Customers.Web.Api.WebAdapters.GetReadonlyCustomersAdapter;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder
    .Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        // This tells the API to look for the version in the URL path
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    });
builder.Services.AddGetCustomer();
builder.Services.AddGetCustomers(builder.Configuration);
builder.Services.AddGetCustomerOrders(builder.Configuration);
builder.Services.AddNorthwindCustomersDatabase(builder.Configuration.GetConnectionString("Northwind"));
builder.Services.AddHealthChecks();
builder.Services.AddScoped<IGetReadOnlyCustomersWebAdapter, GetReadOnlyCustomersWebAdapter>();
builder.Services.AddScoped<IGetReadOnlyCustomerWebAdapter, GetReadOnlyCustomerWebAdapter>();
builder.Services.AddScoped<IGetReadOnlyCustomerOrdersWebAdapter, GetReadOnlyCustomerOrdersWebAdapter>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseHealthChecks("/health", new HealthCheckOptions(){ResponseWriter = (context, report) => context.Response.WriteAsJsonAsync(builder.Environment.EnvironmentName)});
app.UseCors();

app.MapCustomersApi();

app.Run();
