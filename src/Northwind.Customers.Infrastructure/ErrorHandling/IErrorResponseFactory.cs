namespace Northwind.Customers.Infrastructure.ErrorHandling;

public interface IErrorResponseFactory<out TResponse>
{
    TResponse  CreateErrorResponse(Exception ex);
}
