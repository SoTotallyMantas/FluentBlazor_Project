namespace FluentBlazor_Project.Data.Models
{
    public class Favorites
    {
       public Guid Id { get; set; }

       public required string UserId { get; set; }
       public virtual ApplicationUser User { get; set; }

        public required Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

    }
}
