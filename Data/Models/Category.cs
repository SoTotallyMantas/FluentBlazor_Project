using FluentBlazor_Project.Data.Models.ImageTables;

namespace FluentBlazor_Project.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public string ?CategoryName { get; set; }
        public string ?Title { get; set; }
        public string ?Description { get; set; }

        public CategoryImages? Images { get; set; }
    }
}
