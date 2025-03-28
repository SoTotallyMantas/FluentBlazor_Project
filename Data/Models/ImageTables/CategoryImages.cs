﻿namespace FluentBlazor_Project.Data.Models.ImageTables
{
    public class CategoryImages
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }
        

        public required string ImagePath { get; set; }
    }
}
