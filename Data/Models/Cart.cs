namespace FluentBlazor_Project.Data.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        
        public required string UserId { get; set; }

        public virtual ApplicationUser ?User { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

    }
}
