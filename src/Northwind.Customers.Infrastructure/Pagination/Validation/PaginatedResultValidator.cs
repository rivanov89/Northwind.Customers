using Microsoft.Extensions.Options;
using Northwind.Customers.Infrastructure.Abstractions.Models;

namespace Northwind.Customers.Infrastructure.Pagination.Validation
{
    public class PaginatedResultValidator<TClient> : IPagedQueryValidator<TClient>
    {
        private readonly IOptions<PagedQueryValidationOptions<TClient>> _options;

        public PaginatedResultValidator(IOptions<PagedQueryValidationOptions<TClient>> options)
        {
            _options = options;
        }

        public bool IsValid(BasePagedQuery value, out IEnumerable<string>? messages)
        {
            var errors = PaginationError.None;
            if (value.PageIdx < 0)
            {
                errors |= PaginationError.NegativePageIdx;
            }

            if (value.PageSize <= 0)
            {
                errors |= PaginationError.PageSizeNegative;
            }

            if (value.PageSize > _options.Value.MaxPageSize)
            {
                errors |= PaginationError.PageSizeTooLarge;
            }

            if (errors != 0)
            {
                var errorList = new List<string>();
                if (errors.HasFlag(PaginationError.NegativePageIdx))
                {
                    errorList.Add("PageIdx must be greater than or equal to 0.");
                }
                if (errors.HasFlag(PaginationError.PageSizeNegative))
                {
                    errorList.Add("PageSize must be greater than 0");
                }
                if (errors.HasFlag(PaginationError.PageSizeTooLarge))
                {
                    errorList.Add($"PageSize must no exceed the maximum page size limit of {_options.Value.MaxPageSize}");
                }
                messages = errorList;
                    return false;
            }

            messages = null;
            return true;
        }

        [Flags]
        private enum PaginationError
        {
            None = 0,
            NegativePageIdx = 1 << 0,
            PageSizeTooLarge = 1 << 1,
            PageSizeNegative = 1 << 2
        }
    }
}
