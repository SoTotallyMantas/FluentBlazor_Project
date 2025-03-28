﻿using FluentBlazor_Project.Data.Models.ImageTables;
using System.ComponentModel.DataAnnotations;

namespace FluentBlazor_Project.Data.Models
{
    public class Product
    {
       
        public Guid Id { get; set; }

        public required string Type { get; set; }

        
        public required string Name { get; set; }


        public required string Description { get; set; }

        public required string Category { get; set; }
       
        public decimal Price { get; set; }


        public bool IsDeleted { get; set; } = false;


        public List<ProductImages>? Images { get; set; }

    }
}
