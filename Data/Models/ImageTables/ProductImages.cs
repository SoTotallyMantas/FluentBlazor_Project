﻿namespace FluentBlazor_Project.Data.Models.ImageTables
{
    public class ProductImages
    {
        public Guid ImageId { get; set; }

        public Guid ProductId { get; set; }
        public required Product Product { get; set; }
        public enum Tag
        {
            Thumbnail = 1,
            Extras = 2

        }
        public required Tag SelectedTag { get; set; } = Tag.Extras;
        public required string ImagePath { get; set; }
    }
}
