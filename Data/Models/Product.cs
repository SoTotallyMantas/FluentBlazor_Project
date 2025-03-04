using System.ComponentModel.DataAnnotations;

namespace FluentBlazor_Project.Data.Models
{
    public class Product
    {
       
        public Guid Id { get; set; }

        public required string Type { get; set; }

        
        public required string Name { get; set; }


        public required string Description { get; set; }

       
        public decimal Price { get; set; }

       
    }
}
