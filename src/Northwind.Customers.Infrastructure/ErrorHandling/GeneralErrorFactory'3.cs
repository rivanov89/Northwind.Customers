using OneOf;

namespace Northwind.Customers.Infrastructure.ErrorHandling;

public class GeneralErrorFactory<T0, T1, T2> : IErrorResponseFactory<OneOf<T0, T1, T2, GeneralErrorResult>>
{
    public OneOf<T0, T1, T2, GeneralErrorResult> CreateErrorResponse(Exception ex)
    {
        return new GeneralErrorResult();
    }
}