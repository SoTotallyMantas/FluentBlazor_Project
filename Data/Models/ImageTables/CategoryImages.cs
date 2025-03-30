namespace FluentBlazor_Project.Data.Models.ImageTables
{
    public class CategoryImages
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category ?Category { get; set; }
        

        public string ?ImagePath { get; set; }
    }
}
