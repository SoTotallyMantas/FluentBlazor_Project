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

        protected override async void OnInitialized()
        {
            products = await _ProductService.GetProductsAsync();
        }

    }
}
