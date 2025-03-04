namespace FluentBlazor_Project.Data.Models
{
    public class Favorites
    {
       public Guid Id { get; set; }

       public string UserId { get; set; }
       public virtual ApplicationUser User { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;

    }
}
