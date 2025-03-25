using FluentBlazor_Project.Components.Products;
using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Data;
using static FluentBlazor_Project.Components.Products.ProductDataTest;

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
       

        [Parameter] 
        public string Category { get; set; } = string.Empty;

        private string pageTitle = string.Empty;
        private string description = string.Empty;
        private List<Product> filteredProducts = new();

        protected override async Task OnParametersSetAsync()
        {
            
            filteredProducts = await _ProductService.GetProductByCategoryAsync(Category);
            
            

            if (ProductDataTest.productDetails.TryGetValue(Category, out var details))
            {
                pageTitle = details.Title;
                description = details.Description;
            }
            else
            {
                Navigation.NavigateTo("/404");
            }
        }
        
        

    }
}
