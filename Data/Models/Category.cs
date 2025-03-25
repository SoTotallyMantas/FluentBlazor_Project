namespace FluentBlazor_Project.Data.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        public required string CategoryName { get; set; }
        public string ?Title { get; set; }
        public string ?Description { get; set; }
    }
}
