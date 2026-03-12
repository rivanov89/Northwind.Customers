using Microsoft.AspNetCore.Mvc;
using Northwind.Customers.Web.Models;
using Northwind.Customers.Web.Services.Customers;

namespace Northwind.Customers.Web.Controllers;

public class HomeController : Controller
{
    private readonly ICustomerProxy _customerProxy;

    public HomeController(ICustomerProxy customerProxy)
    {
        _customerProxy = customerProxy;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("/GetList")]
    public async Task<IActionResult> GetList(string searchTerm, int page, int pageSize, CancellationToken token)
    {
        var customers = await _customerProxy.GetCustomersAsync(searchTerm, page, pageSize, token);
        var html = await this.RenderViewAsync("_CustomerTableRows",
            customers.Data.Select(dto => new CustomerRowViewModel
            {
                CustomerId = dto.CustomerId,
                CustomerName = dto.Name,
                OrderCount = dto.TotalOrderCount
            }).ToList(),
            true);

        return Json(new
        {
            html,
            totalPages = customers.PageCount, 
            currentPage = page 
        });
    }

    public async Task<IActionResult> Details(string id, CancellationToken token)
    {
        if (string.IsNullOrEmpty(id))
            return NotFound();

        var customer = await _customerProxy.GetCustomerByIdAsync(id, token);

        if (customer == null)
        {
            return NotFound();
        }

        var viewModel = new CustomerViewModel
        {
            CustomerId = customer.CustomerId,
            CompanyName = customer.CompanyName,
            ContactName = customer.ContactName, 
            Address = customer.Address,
            City = customer.City,
            Region = customer.Region,
            PostalCode = customer.PostalCode,
            Country = customer.Country,
            Phone = customer.Phone,
            Fax = customer.Fax
        };

        return View(viewModel);
    }

    [HttpGet("/Orders")]
    public async Task<IActionResult> GetOrders(string customerId, int page, int pageSize, CancellationToken token)
    {
        if (string.IsNullOrEmpty(customerId))
            return NotFound();

        var orders = await _customerProxy.GetCustomerOrdersAsync(customerId, page, pageSize, token);
        var html = await this.RenderViewAsync(
            "_OrderList",
            orders
                .Data
                .Select(dto => new OrderViewModel
                {
                    InStockIssue = dto.InStockIssue,
                    AnyProductDiscontinued = dto.AnyProductDiscontinued,
                    ProductCount = dto.ProductCount,
                    Total = dto.Total
                }).ToList(),
            true);

        return Json(new
        {
            html,
            totalPages = orders.PageCount,
            currentPage = page
        });
    }
}
