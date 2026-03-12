using System.ComponentModel.DataAnnotations;

namespace Northwind.Customers.Web.Models
{
    public class CustomerViewModel
    {
        [Required]
        public string CustomerId { get; set; } = string.Empty;

        [Required]
        [StringLength(40)]
        public string CompanyName { get; set; } = string.Empty;

        [StringLength(30)]
        public string? ContactName { get; set; }

        [StringLength(30)]
        public string? ContactTitle { get; set; }

        [StringLength(60)]
        public string? Address { get; set; }

        [StringLength(15)]
        public string? City { get; set; }

        [StringLength(15)]
        public string? Region { get; set; }

        [StringLength(10)]
        public string? PostalCode { get; set; }

        [StringLength(15)]
        public string? Country { get; set; }

        [Phone]
        [StringLength(24)]
        public string? Phone { get; set; }

        [Phone]
        [StringLength(24)]
        public string? Fax { get; set; }
    }
}