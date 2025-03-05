namespace FluentBlazor_Project.Data.Models
{
    // Product Filtering options for Product Service
    public class ProductFilterOptions
    {
        public List<decimal>? PriceRange { get; set; }

        public List<string>? categories { get; set; }

        public string? ProductName { get; set; }
    }
}
