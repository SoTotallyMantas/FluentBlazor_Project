using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using Microsoft.AspNetCore.Components;

namespace FluentBlazor_Project.Components.Pages
{
    public partial class Products
    {
        private IProductService _ProductService { get; set; }
        private List<Product> products = [];
        public Products(IProductService productService)
        {
            _ProductService = productService;
        }

        public void addSampleProducts()
        {
            List<Product> productSample = new List<Product>
            {
                 new Product
        {
            
            Type = "Electronics",
            Name = "Laptop X200",
            Description = "A high-performance laptop with 16GB RAM and 512GB SSD.",
            Category = "Computers",
            Price = 1200.00M
        },
        new Product
        {
            
            Type = "Electronics",
            Name = "Smartphone A10",
            Description = "A budget-friendly smartphone with 64GB storage.",
            Category = "Mobile Phones",
            Price = 300.00M
        },
        new Product
        {
          
            Type = "Appliance",
            Name = "Air Fryer Pro",
            Description = "A modern air fryer with 5 cooking modes.",
            Category = "Kitchen Appliances",
            Price = 150.00M
        },
        new Product
        {
            
            Type = "Clothing",
            Name = "Leather Jacket",
            Description = "Stylish leather jacket for casual and formal wear.",
            Category = "Fashion",
            Price = 250.00M
        },
        new Product
        {
           
            Type = "Home Decor",
            Name = "Table Lamp",
            Description = "A minimalist table lamp with adjustable brightness.",
            Category = "Furniture",
            Price = 75.00M
        },
        new Product
        {
          
            Type = "Electronics",
            Name = "Wireless Headphones",
            Description = "Noise-cancelling wireless headphones with 30-hour battery life.",
            Category = "Audio",
            Price = 200.00M
        },
        new Product
        {
           
            Type = "Appliance",
            Name = "Smart Thermostat",
            Description = "A smart thermostat with voice control and mobile app integration.",
            Category = "Home Automation",
            Price = 180.00M
        },
        new Product
        {
           
            Type = "Clothing",
            Name = "Running Shoes",
            Description = "Lightweight running shoes with extra cushioning.",
            Category = "Sportswear",
            Price = 120.00M
        },
        new Product
        {
            
            Type = "Furniture",
            Name = "Ergonomic Chair",
            Description = "A comfortable ergonomic office chair with lumbar support.",
            Category = "Office Furniture",
            Price = 300.00M
        },
        new Product
        {
           
            Type = "Electronics",
            Name = "Smartwatch S10",
            Description = "A smartwatch with health tracking and GPS functionality.",
            Category = "Wearables",
            Price = 250.00M
        }

            };
            foreach(var product in productSample)
            {
                _ProductService.AddProuctAsync(product);
            };
        }



        [Parameter] public string Category { get; set; }

        private string pageTitle;
        private string description;

        protected override void OnParametersSet()
        {
            var productDetails = new Dictionary<string, (string Title, string Description)>
        {
            { "CPU", ("Processors", "Details about CPUs") },
            { "GPU", ("Graphics Cards", "Details about GPUs") },
            { "PCU", ("Power Block", "Details about Power Supply Units") },
            { "MB", ("Motherboard", "Details about Motherboards") },
            { "RAM", ("RAM Memory", "Details about RAM Modules") },
            { "Storage", ("Storage", "Details about Storage Devices") }
        };

            if (productDetails.TryGetValue(Category, out var details))
            {
                pageTitle = details.Title;
                description = details.Description;
            }
            else
            {
                Navigation.NavigateTo("/404");
            }
        }

        protected override async Task OnInitializedAsync()
        {
            products = await _ProductService.GetProductsAsync();
           
        }

    }
}
