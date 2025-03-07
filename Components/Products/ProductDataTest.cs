namespace FluentBlazor_Project.Components.Products
{
    public class ProductDataTest
    {

        public static readonly Dictionary<string, (string Title, string Description)> productDetails = new()
        {
            { "CPU", ("Processors", "Details about CPUs") },
            { "GPU", ("Graphics Cards", "Details about GPUs") },
            { "PSU", ("Power Block", "Details about Power Supply Units") },
            { "MB", ("Motherboard", "Details about Motherboards") },
            { "RAM", ("RAM Memory", "Details about RAM Modules") },
            { "Storage", ("Storage", "Details about Storage Devices") }
        };

        public static readonly List<ProductTest> allProducts = new()
        {
            new ProductTest { Id = 1, Type = "Processors", Name = "Intel Core i7", Category = "CPU", Price = 300 },
            new ProductTest { Id = 2, Type = "Processors", Name = "AMD Ryzen 5", Category = "CPU", Price = 250 },
            new ProductTest { Id = 3, Type = "Graphics Cards", Name = "NVIDIA RTX 4070", Category = "GPU", Price = 700 },
            new ProductTest { Id = 4, Type = "Graphics Cards", Name = "AMD RX 6800", Category = "GPU", Price = 650 },
            new ProductTest { Id = 5, Type = "RAM Memory", Name = "Corsair 16GB DDR4", Category = "RAM", Price = 20 },
            new ProductTest { Id = 6, Type = "RAM Memory", Name = "G.Skill 8GB DDR4", Category = "RAM", Price = 15 },
            new ProductTest { Id = 7, Type = "Power Block", Name = "Gigabyte 450w", Category = "PSU", Price = 40 },
            new ProductTest { Id = 8, Type = "Power Block", Name = "CHIEFTEC ECO 500w", Category = "PSU", Price = 42 },
            new ProductTest { Id = 9, Type = "MotherBoard", Name = "GIGABYTE Intel Z790", Category = "MB", Price = 188 },
            new ProductTest { Id = 10, Type = "MotherBoard", Name = "ASUS AMD B650", Category = "MB", Price = 184 },
            new ProductTest { Id = 11, Type = "Storage", Name = "ADATA SU650 512GB", Category = "Storage", Price = 27 },
            new ProductTest { Id = 12, Type = "Storage", Name = "WD Blue 1TB", Category = "Storage", Price = 59 },
            new ProductTest { Id = 13, Type = "Processors", Name = "Intel Core i7", Category = "CPU", Price = 300 },
            new ProductTest { Id = 14, Type = "Processors", Name = "AMD Ryzen 5", Category = "CPU", Price = 250 },
            new ProductTest { Id = 15, Type = "Graphics Cards", Name = "NVIDIA RTX 4070", Category = "GPU", Price = 700 },
            new ProductTest { Id = 16, Type = "Graphics Cards", Name = "AMD RX 6800", Category = "GPU", Price = 650 },
            new ProductTest { Id = 17, Type = "RAM Memory", Name = "Corsair 16GB DDR4", Category = "RAM", Price = 20 },
            new ProductTest { Id = 18, Type = "RAM Memory", Name = "G.Skill 8GB DDR4", Category = "RAM", Price = 15 },
            new ProductTest { Id = 19, Type = "Power Block", Name = "Gigabyte 450w", Category = "PSU", Price = 40 },
            new ProductTest { Id = 20, Type = "Power Block", Name = "CHIEFTEC ECO 500w", Category = "PSU", Price = 42 },
            new ProductTest { Id = 21, Type = "MotherBoard", Name = "GIGABYTE Intel Z790", Category = "MB", Price = 188 },
            new ProductTest { Id = 22, Type = "MotherBoard", Name = "ASUS AMD B650", Category = "MB", Price = 184 },
            new ProductTest { Id = 23, Type = "Storage", Name = "ADATA SU650 512GB", Category = "Storage", Price = 27 },
            new ProductTest { Id = 24, Type = "Storage", Name = "WD Blue 1TB", Category = "Storage", Price = 59 },
            new ProductTest { Id = 25, Type = "Storage", Name = "ADATA SU650 512GB", Category = "Storage", Price = 27 },
            new ProductTest { Id = 26, Type = "Storage", Name = "WD Blue 1TB", Category = "Storage", Price = 59 }
        };
        public class ProductTest
        {
            public int Id { get; set; }
            public string Type { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public decimal Price { get; set; }
        }
    }
}
