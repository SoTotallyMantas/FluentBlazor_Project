using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;

namespace FluentBlazor_Project.Components.Pages
{
    public partial class Products
    {
        private IProductService _ProductService { get; set; }
        private List<Product> products;
        public Products(IProductService productService)
        {
            _ProductService = productService;
        }

        protected override async void OnInitialized()
        {
            products = await _ProductService.GetProductsAsync();
        }

    }
}
