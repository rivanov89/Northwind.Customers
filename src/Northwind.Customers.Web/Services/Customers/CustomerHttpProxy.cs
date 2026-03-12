using System.Text;
using Microsoft.Extensions.Options;
using Northwind.Customers.Web.DomainModels;

namespace Northwind.Customers.Web.Services.Customers;

public class CustomerHttpProxy : ICustomerProxy
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<CustomersApiOptions> _options;

    public CustomerHttpProxy(HttpClient httpClient, IOptions<CustomersApiOptions> options)
    {
        _httpClient = httpClient;
        _options = options;
    }

    public Task<CustomerPaginatedResponse> GetCustomersAsync(string? name, int pageIdx, int pageSize, CancellationToken token)
    {
        var queryString = new StringBuilder($"{Format(_options.Value.GetCustomersTemplate, 
            [new KeyValuePair<string, string>("{version}", _options.Value.ApiVersion.ToString())])}?");
        if (!string.IsNullOrEmpty(name))
        {
            queryString.Append($"name={name}&");
        }

        queryString.Append($"pageIdx={pageIdx-1}&");
        queryString.Append($"pageSize={pageSize}");

        return _httpClient.GetFromJsonAsync<CustomerPaginatedResponse>(queryString.ToString(), token);
    }

    public Task<CustomerDetailDto> GetCustomerByIdAsync(string customerId, CancellationToken token)
    {
        var relativeUrl = Format(_options.Value.GetCustomerTemplate, [new KeyValuePair<string, string>("{version}", _options.Value.ApiVersion.ToString()), new KeyValuePair<string, string>("{customerId}", customerId)]);
       return _httpClient.GetFromJsonAsync<CustomerDetailDto>(relativeUrl, token);
    }

    public Task<CustomerOrdersPaginatedResponse> GetCustomerOrdersAsync(string customerId, int pageIdx, int pageSize, CancellationToken token)
    {
        var queryString = new StringBuilder($"{Format(_options.Value.GetCustomerOrdersTemplate,
            [new KeyValuePair<string, string>("{version}", _options.Value.ApiVersion.ToString()), new KeyValuePair<string, string>("{customerId}", customerId)])}?");
        queryString.Append($"pageIdx={pageIdx-1}&");
        queryString.Append($"pageSize={pageSize}");

        return _httpClient.GetFromJsonAsync<CustomerOrdersPaginatedResponse>(queryString.ToString(), token);
    }

    private string Format(string template, IEnumerable<KeyValuePair<string, string>> replacements)
    {
        if (string.IsNullOrEmpty(template)) return string.Empty;

        var totalLength = template.Length;
        var replacementList = new List<(int Index, int KeyLen, string Value)>();

        foreach (var (placeholder, value) in replacements)
        {
            var index = template.IndexOf(placeholder, StringComparison.Ordinal);

            if (index != -1)
            {
                replacementList.Add((index, placeholder.Length, Value: value));
                totalLength += (value.Length - placeholder.Length);
            }
        }

        replacementList.Sort((a, b) => a.Index.CompareTo(b.Index));

        var currentDestIndex = 0;
        return string.Create(totalLength, (template, replacementList), (dest, state) =>
        {
            var source = state.template.AsSpan();
            var lastSourceIndex = 0;

            foreach (var (index, keyLen, value) in state.replacementList)
            {
                var staticTextLen = index - lastSourceIndex;
                if (staticTextLen > 0)
                {
                    source.Slice(lastSourceIndex, staticTextLen).CopyTo(dest.Slice(currentDestIndex));
                    currentDestIndex += staticTextLen;
                }

                value.AsSpan().CopyTo(dest.Slice(currentDestIndex));
                currentDestIndex += value.Length;
                lastSourceIndex = index + keyLen;
            }

            if (lastSourceIndex < source.Length)
            {
                source.Slice(lastSourceIndex).CopyTo(dest.Slice(currentDestIndex));
            }
        });
    }
}