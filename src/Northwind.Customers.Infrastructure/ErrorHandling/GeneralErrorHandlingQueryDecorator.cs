using Microsoft.Extensions.Logging;
using Northwind.Customers.Infrastructure.Abstractions;

namespace Northwind.Customers.Infrastructure.ErrorHandling
{
    public class GeneralErrorHandlingQueryDecorator<TQuery, TResponse> : IQuery<TQuery, TResponse>
    {
        private readonly IQuery<TQuery, TResponse> _next;
        private readonly ILogger<GeneralErrorHandlingQueryDecorator<TQuery, TResponse>> _logger;
        private readonly IErrorResponseFactory<TResponse> _errorResponseFactory;

        public GeneralErrorHandlingQueryDecorator(IQuery<TQuery, TResponse> next, 
            ILogger<GeneralErrorHandlingQueryDecorator<TQuery, TResponse>> logger,
            IErrorResponseFactory<TResponse> errorResponseFactory)
        {
            _next = next;
            _logger = logger;
            _errorResponseFactory = errorResponseFactory;
        }
        public async Task<TResponse> ExecuteAsync(TQuery query, CancellationToken token)
        {
            try
            {
                return await _next.ExecuteAsync(query, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while executing query {QueryType}", typeof(TQuery).Name);
                return _errorResponseFactory.CreateErrorResponse(e);
            }
        }
    }
}
