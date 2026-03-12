using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Northwind.Customers.Data.EntityFrameworkCore.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddNorthwindCustomersDatabase(this IServiceCollection services, string connectionString)
        {
            return services.AddDbContext<NorthwindContext>(builder =>
            {
                builder
                    .UseSqlServer(connectionString);
            });
        }
    }
}
