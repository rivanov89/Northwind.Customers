namespace Northwind.Customers.Infrastructure.Abstractions.Models;

public abstract class BaseValidationResult
{
    protected BaseValidationResult(IEnumerable<string> validationFailureMessages)
    {
        ValidationFailureMessages = validationFailureMessages;
    }

    public IEnumerable<string> ValidationFailureMessages { get; }
}