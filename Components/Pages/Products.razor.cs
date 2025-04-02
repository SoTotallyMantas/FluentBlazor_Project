using FluentBlazor_Project.Components.Products;
using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using Microsoft.AspNetCore.Components;

namespace FluentBlazor_Project.Components.Pages
{
    public partial class Products
    {
        [Parameter]
        public string Category { get; set; } = string.Empty;

        [Inject]
        private IProductService ProductService { get; set; } = default!;

        [Inject]
        private ICategoryService CategoryService { get; set; } = default!;

        private List<Product> filteredProducts = new();
        private string pageTitle = string.Empty;

        protected override async Task OnParametersSetAsync()
        {
            var categories = await CategoryService.GetCategoriesAsync();
            var selectedCategory = categories.FirstOrDefault(c => c.CategoryName.Equals(Category, StringComparison.OrdinalIgnoreCase));

            if (selectedCategory != null)
            {
                pageTitle = selectedCategory.Title;
                filteredProducts = await ProductService.GetProductByCategoryAsync(Category);
            }
            else
            {
                pageTitle = Localizer["categoryNotFound"];
                filteredProducts.Clear();
            }
        }
    }
}
