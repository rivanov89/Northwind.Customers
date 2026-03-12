namespace Northwind.Customers.Infrastructure.Abstractions
{
    public interface IValidator<in T>
    {
        bool IsValid(T value, out IEnumerable<string>? messages);
    }
}
