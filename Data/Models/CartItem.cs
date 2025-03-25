namespace FluentBlazor_Project.Data.Models
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public Guid CartId { get; set; }
        public required Cart Cart { get; set; }

        public Guid ProductId { get; set; }
        public Product ?Product { get; set; }
        public int Quantity { get; set; }

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
    }
}
