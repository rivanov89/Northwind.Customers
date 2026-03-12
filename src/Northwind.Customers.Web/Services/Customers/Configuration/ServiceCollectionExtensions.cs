using Microsoft.Extensions.Options;

namespace Northwind.Customers.Web.Services.Customers.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CustomersApiOptions>(configuration.GetSection("CustomersApiOptions"));
            services.AddScoped<ICustomerProxy, CustomerHttpProxy>();

            services.AddHttpClient<ICustomerProxy, CustomerHttpProxy>((serviceProvider, client) =>
                {
                    var options = serviceProvider.GetRequiredService<IOptions<CustomersApiOptions>>().Value;
                    client.BaseAddress = new Uri(options.BaseAddress);
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                    {
                        if (errors == System.Net.Security.SslPolicyErrors.None) return true;

                        // Allow mismatch only for specific local development hosts
                        if (errors == System.Net.Security.SslPolicyErrors.RemoteCertificateNameMismatch)
                        {
                            return cert.Subject.Contains("localhost");
                        }

                        return false;
                    }
                });
            return services;
        }
    }
}
