namespace BilliardsManager.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; }

        // Navigation property 
        public ICollection<TicketPurchase> TicketPurchases { get; set; } = [];

    }
}
