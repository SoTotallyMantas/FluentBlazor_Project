using System.ComponentModel.DataAnnotations;

namespace FluentBlazor_Project.Data.Models
{
    public class Purchase
    {
        
        public Guid Id { get; set; }

        
        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; } = new List<PurchaseItem>();

        // Allowing null for anonymization
        public string? UserId { get; set; }

        public virtual ApplicationUser? User { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
    }
}
