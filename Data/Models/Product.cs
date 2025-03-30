using FluentBlazor_Project.Data.Models.ImageTables;
using System.ComponentModel.DataAnnotations;

namespace FluentBlazor_Project.Data.Models
{
    public class Product
    {
       
        public Guid Id { get; set; }

        public  string Type { get; set; }

        
        public string Name { get; set; }


        public string Description { get; set; }

        public string Category { get; set; }
       
        public decimal Price { get; set; }


        public bool IsDeleted { get; set; } = false;


        public List<ProductImages>? Images { get; set; }

    }
}
