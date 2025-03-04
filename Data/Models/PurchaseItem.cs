namespace FluentBlazor_Project.Data.Models
{
    public class PurchaseItem
    {

        public Guid PurchaseId { get; set; }

        public virtual Purchase Purchase { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
