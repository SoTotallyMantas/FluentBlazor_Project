using FluentBlazor_Project.Data.Models;
using FluentBlazor_Project.Interface;
using FluentBlazor_Project.Services;

namespace FluentBlazor_Project.Components.Products
{
    public class ProductDataTest
    {
        private IProductService _ProductService { get; set; } 

        public ProductDataTest(IProductService productService) {
            _ProductService = productService;
        }
        public static readonly Dictionary<string, (string Title, string Description)> productDetails = new()
        {
            { "CPU", ("Processors", "Details about CPUs") },
            { "GPU", ("Graphics Cards", "Details about GPUs") },
            { "PSU", ("Power Block", "Details about Power Supply Units") },
            { "MB", ("Motherboard", "Details about Motherboards") },
            { "RAM", ("RAM Memory", "Details about RAM Modules") },
            { "Storage", ("Storage", "Details about Storage Devices") }
        };

        public static readonly List<Product> allProducts = new()
        {
            new Product
            {
                
                Type = "Processors",
                Name = "Intel Core i7",
                Category = "CPU",
                Price = 300,
                Description =
                    "<p><strong>The Intel Core i7-12700K</strong> is an 12th generation processor from Intel's Alder Lake series. It features a hybrid architecture with a combination of high-performance cores and efficiency cores to provide the best balance between performance and power consumption. Ideal for gaming and multitasking, it supports overclocking and comes with Intel's latest technologies like DDR5 memory support and PCIe 5.0 compatibility.</p>" +
                    "<p>This processor is a great choice for enthusiasts looking to build a powerful PC that can handle demanding tasks like gaming, video editing, and 3D rendering. With 12 cores and 20 threads, it offers excellent performance across all workloads.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Model:</strong> Intel Core i7-12700K</li>" +
                        "<li><strong>Cores/Threads:</strong> 12 Cores / 20 Threads</li>" +
                        "<li><strong>Base Clock:</strong> 3.6 GHz</li>" +
                        "<li><strong>Boost Clock:</strong> 5.0 GHz</li>" +
                        "<li><strong>Socket:</strong> LGA 1700</li>" +
                        "<li><strong>TDP:</strong> 125W</li>" +
                        "<li><strong>Cache:</strong> 25MB Intel Smart Cache</li>" +
                        "<li><strong>Supported Memory:</strong> DDR5 / DDR4</li>" +
                        "<li><strong>PCIe Support:</strong> PCIe 5.0</li>" +
                    "</ul>"
            },

            new Product
            {
                
                Type = "Processors",
                Name = "AMD Ryzen 5",
                Category = "CPU",
                Price = 250,
                Description =
                    "<p><strong>The AMD Ryzen 5 5600X</strong> is a 5th generation processor from AMD's Ryzen series, built on the Zen 3 architecture. It offers excellent performance for gaming, productivity, and multitasking with a focus on efficiency and value. This CPU is perfect for mid-range builds and supports PCIe 4.0 for fast storage and graphics.</p>" +
                    "<p>With 6 cores and 12 threads, the Ryzen 5 5600X delivers strong single-threaded and multi-threaded performance, making it a favorite among gamers and content creators on a budget.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Model:</strong> AMD Ryzen 5 5600X</li>" +
                        "<li><strong>Cores/Threads:</strong> 6 Cores / 12 Threads</li>" +
                        "<li><strong>Base Clock:</strong> 3.7 GHz</li>" +
                        "<li><strong>Boost Clock:</strong> 4.6 GHz</li>" +
                        "<li><strong>Socket:</strong> AM4</li>" +
                        "<li><strong>TDP:</strong> 65W</li>" +
                        "<li><strong>Cache:</strong> 32MB L3 Cache</li>" +
                        "<li><strong>Supported Memory:</strong> DDR4</li>" +
                        "<li><strong>PCIe Support:</strong> PCIe 4.0</li>" +
                    "</ul>"
            },

            new Product
            {
                
                Type = "Graphics Cards",
                Name = "NVIDIA RTX 4070",
                Category = "GPU",
                Price = 700,
                Description =
                    "<p><strong>The NVIDIA GeForce RTX 4070</strong> is a high-performance graphics card from NVIDIA's Ada Lovelace architecture. It delivers exceptional gaming and rendering capabilities with support for ray tracing and DLSS 3, making it ideal for 1440p and 4K gaming.</p>" +
                    "<p>This GPU balances power and efficiency, offering 12GB of GDDR6X memory and advanced AI features for gamers and creators who demand top-tier visuals.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Model:</strong> NVIDIA GeForce RTX 4070</li>" +
                        "<li><strong>Memory:</strong> 12GB GDDR6X</li>" +
                        "<li><strong>Core Clock:</strong> 2.5 GHz</li>" +
                        "<li><strong>Power Consumption:</strong> 200W</li>" +
                        "<li><strong>Ray Tracing:</strong> Yes</li>" +
                        "<li><strong>PCIe Interface:</strong> PCIe 4.0</li>" +
                        "<li><strong>Memory Bandwidth:</strong> 504 GB/s</li>" +
                        "<li><strong>CUDA Cores:</strong> 5888</li>" +
                    "</ul>"
            },

            new Product
            {
                
                Type = "Graphics Cards",
                Name = "AMD RX 6800",
                Category = "GPU",
                Price = 650,
                Description =
                    "<p><strong>The AMD Radeon RX 6800</strong> is a powerful graphics card from AMD's RDNA 2 architecture. Designed for high-end gaming and content creation, it supports ray tracing and offers 16GB of GDDR6 memory for smooth performance at high resolutions.</p>" +
                    "<p>This GPU is a great choice for enthusiasts seeking excellent value and performance, competing strongly in 1440p and 4K gaming scenarios.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Model:</strong> AMD Radeon RX 6800</li>" +
                        "<li><strong>Memory:</strong> 16GB GDDR6</li>" +
                        "<li><strong>Core Clock:</strong> 1.8 GHz</li>" +
                        "<li><strong>Boost Clock:</strong> 2.1 GHz</li>" +
                        "<li><strong>Power Consumption:</strong> 250W</li>" +
                        "<li><strong>Ray Tracing:</strong> Yes</li>" +
                        "<li><strong>PCIe Interface:</strong> PCIe 4.0</li>" +
                        "<li><strong>Compute Units:</strong> 60</li>" +
                    "</ul>"
            },

            new Product
            {
                Type = "RAM Memory",
                Name = "Corsair 16GB DDR4",
                Category = "RAM",
                Price = 20,
                Description =
                    "<p><strong>The Corsair 16GB DDR4</strong> is a reliable and high-performance memory module designed for modern PCs. With a speed of 3200MHz, it enhances system responsiveness for gaming, multitasking, and productivity tasks.</p>" +
                    "<p>This RAM is perfect for budget builds or upgrades, offering a balance of capacity and speed with Corsair's trusted quality.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Brand:</strong> Corsair</li>" +
                        "<li><strong>Type:</strong> DDR4</li>" +
                        "<li><strong>Capacity:</strong> 16GB</li>" +
                        "<li><strong>Speed:</strong> 3200MHz</li>" +
                        "<li><strong>Voltage:</strong> 1.35V</li>" +
                        "<li><strong>Latency:</strong> CL16</li>" +
                        "<li><strong>Form Factor:</strong> DIMM</li>" +
                    "</ul>"
            },

            new Product
            {
      
                Type = "RAM Memory",
                Name = "G.Skill 8GB DDR4",
                Category = "RAM",
                Price = 15,
                Description =
                    "<p><strong>The G.Skill 8GB DDR4</strong> is a cost-effective memory solution for entry-level PCs and upgrades. Running at 3000MHz, it provides solid performance for everyday computing and light gaming.</p>" +
                    "<p>This module is ideal for users seeking affordable RAM with dependable G.Skill quality, perfect for smaller systems or budget builds.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Brand:</strong> G.Skill</li>" +
                        "<li><strong>Type:</strong> DDR4</li>" +
                        "<li><strong>Capacity:</strong> 8GB</li>" +
                        "<li><strong>Speed:</strong> 3000MHz</li>" +
                        "<li><strong>Voltage:</strong> 1.2V</li>" +
                        "<li><strong>Latency:</strong> CL16</li>" +
                        "<li><strong>Form Factor:</strong> DIMM</li>" +
                    "</ul>"
            },

            new Product
            {
   
                Type = "Power Block",
                Name = "Gigabyte 450W",
                Category = "PSU",
                Price = 40,
                Description =
                    "<p><strong>The Gigabyte 450W</strong> is a reliable power supply unit designed for budget-friendly PC builds. Certified with 80 PLUS Bronze efficiency, it ensures stable power delivery for mid-range systems.</p>" +
                    "<p>With a 120mm fan for cooling and a 3-year warranty, this PSU is a solid choice for basic gaming or office PCs.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Brand:</strong> Gigabyte</li>" +
                        "<li><strong>Wattage:</strong> 450W</li>" +
                        "<li><strong>Efficiency:</strong> 80 PLUS Bronze</li>" +
                        "<li><strong>Modular:</strong> No</li>" +
                        "<li><strong>Cooling:</strong> 120mm Fan</li>" +
                        "<li><strong>Warranty:</strong> 3 years</li>" +
                        "<li><strong>Connectors:</strong> 20+4 Pin, 4+4 Pin CPU, SATA, Molex</li>" +
                    "</ul>"
            },

            new Product
            {

                Type = "Power Block",
                Name = "CHIEFTEC ECO 500W",
                Category = "PSU",
                Price = 42,
                Description =
                    "<p><strong>The CHIEFTEC ECO 500W</strong> is an efficient power supply unit tailored for cost-conscious builders. With 80 PLUS certification, it provides dependable power for mid-tier desktops.</p>" +
                    "<p>Featuring a quiet 120mm fan and a 3-year warranty, this PSU is perfect for everyday computing needs with a slight edge in wattage over entry-level options.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Brand:</strong> CHIEFTEC</li>" +
                        "<li><strong>Wattage:</strong> 500W</li>" +
                        "<li><strong>Efficiency:</strong> 80 PLUS</li>" +
                        "<li><strong>Modular:</strong> No</li>" +
                        "<li><strong>Cooling:</strong> 120mm Fan</li>" +
                        "<li><strong>Warranty:</strong> 3 years</li>" +
                        "<li><strong>Connectors:</strong> 24 Pin, 4+4 Pin CPU, SATA, Molex</li>" +
                    "</ul>"
            },

            new Product
            {
  
                Type = "MotherBoard",
                Name = "GIGABYTE Intel Z790",
                Category = "MB",
                Price = 188,
                Description =
                    "<p><strong>The GIGABYTE Intel Z790</strong> is a premium motherboard designed for Intel’s 12th and 13th generation processors. It supports DDR5 memory and PCIe 5.0, making it future-proof for high-performance builds.</p>" +
                    "<p>Ideal for gaming and professional workstations, this board offers robust connectivity and storage options with the Z790 chipset.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Model:</strong> GIGABYTE Intel Z790</li>" +
                        "<li><strong>Socket:</strong> LGA 1700</li>" +
                        "<li><strong>Chipset:</strong> Z790</li>" +
                        "<li><strong>Memory Support:</strong> DDR5, up to 128GB</li>" +
                        "<li><strong>PCIe Slots:</strong> PCIe 5.0</li>" +
                        "<li><strong>Storage:</strong> M.2, SATA 6Gb/s</li>" +
                        "<li><strong>Form Factor:</strong> ATX</li>" +
                    "</ul>"
            },

            new Product
            {

                Type = "MotherBoard",
                Name = "ASUS AMD B650",
                Category = "MB",
                Price = 184,
                Description =
                    "<p><strong>The ASUS AMD B650</strong> is a versatile motherboard for AMD’s Ryzen 7000 series processors. With support for DDR5 and PCIe 4.0, it’s built for performance-driven PC setups.</p>" +
                    "<p>This board is great for gamers and creators, offering a balance of modern features and affordability on the AM5 platform.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Model:</strong> ASUS AMD B650</li>" +
                        "<li><strong>Socket:</strong> AM5</li>" +
                        "<li><strong>Chipset:</strong> B650</li>" +
                        "<li><strong>Memory Support:</strong> DDR5, up to 128GB</li>" +
                        "<li><strong>PCIe Slots:</strong> PCIe 4.0</li>" +
                        "<li><strong>Storage:</strong> M.2, SATA 6Gb/s</li>" +
                        "<li><strong>Form Factor:</strong> ATX</li>" +
                    "</ul>"
            },

            new Product
            {

                Type = "Storage",
                Name = "ADATA Ultimate SU650 512GB",
                Category = "Storage",
                Price = 50,
                Description =
                    "<p><strong>The ADATA Ultimate SU650 512GB</strong> is a budget-friendly SSD that boosts system performance with fast read/write speeds. Using 3D NAND technology, it’s perfect for upgrading laptops or desktops.</p>" +
                    "<p>This drive offers reliable storage for everyday use, gaming, and light productivity tasks with a SATA interface.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Model:</strong> ADATA Ultimate SU650</li>" +
                        "<li><strong>Type:</strong> Solid State Drive (SSD)</li>" +
                        "<li><strong>Capacity:</strong> 512GB</li>" +
                        "<li><strong>Interface:</strong> SATA 6Gb/s</li>" +
                        "<li><strong>Form Factor:</strong> 2.5\"</li>" +
                        "<li><strong>Data Transfer Rate:</strong> 600 MBps</li>" +
                        "<li><strong>Features:</strong> 3D NAND, SLC Cache, Error Correction Code (ECC)</li>" +
                        "<li><strong>Dimensions:</strong> 69.85mm x 100.45mm x 7mm</li>" +
                        "<li><strong>Weight:</strong> 59.5g</li>" +
                        "<li><strong>Warranty:</strong> 3 years</li>" +
                    "</ul>"
            },

            new Product
            {
  
                Type = "Storage",
                Name = "Samsung 970 EVO Plus 1TB",
                Category = "Storage",
                Price = 120,
                Description =
                    "<p><strong>The Samsung 970 EVO Plus 1TB</strong> is a high-performance NVMe SSD designed for speed and durability. With V-NAND technology and blazing-fast sequential speeds, it’s ideal for gaming, video editing, and heavy workloads.</p>" +
                    "<p>This drive offers premium storage performance for enthusiasts and professionals, backed by Samsung’s renowned reliability.</p><hr>" +
                    "<ul><strong>Specifications:</strong>" +
                        "<li><strong>Model:</strong> Samsung 970 EVO Plus</li>" +
                        "<li><strong>Type:</strong> NVMe Solid State Drive (SSD)</li>" +
                        "<li><strong>Capacity:</strong> 1TB</li>" +
                        "<li><strong>Interface:</strong> PCIe 3.0 x4, NVMe 1.3</li>" +
                        "<li><strong>Form Factor:</strong> M.2 2280</li>" +
                        "<li><strong>Sequential Read Speed:</strong> Up to 3,500 MB/s</li>" +
                        "<li><strong>Sequential Write Speed:</strong> Up to 3,300 MB/s</li>" +
                        "<li><strong>Features:</strong> V-NAND, Samsung Phoenix Controller, AES 256-bit Encryption</li>" +
                        "<li><strong>Warranty:</strong> 5 years</li>" +
                    "</ul>"
            }
        };

        public async Task AddSampleToDatabase()
        {

            foreach (Product product in allProducts)
            {
               await _ProductService.AddProductAsync(product);
            }
        }

    }
}
