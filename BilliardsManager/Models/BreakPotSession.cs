using BilliardsManager.Models;

public class BreakPotSession
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;
    public decimal StartingPot { get; set; }
    public decimal TicketSalesRevenue { get; set; }
    public decimal TotalPot => StartingPot + TicketSalesRevenue;

    // The player who got drawn for the break
    public int? DrawerPlayerId { get; set; }
    public Player? DrawerPlayer { get; set; }

    public int BallsMade { get; set; } = 0; // 0 to 10
    public decimal PayoutAmount { get; set; }
    public decimal EndingPot { get; set; } // Rolls over to next week

    public ICollection<TicketPurchase> TicketPurchases { get; set; } = [];
}