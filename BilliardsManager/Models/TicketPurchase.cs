using BilliardsManager.Models;

public class TicketPurchase
{
    public int Id { get; set; }
    public int BreakPotSessionId { get; set; }
    public BreakPotSession BreakPotSession { get; set; } = null!;

    public int PlayerId { get; set; }
    public Player Player { get; set; } = null!;

    public decimal TierAmount { get; set; } // e.g. $5, $10, $20
    public int TicketCount { get; set; }  // e.g. 7, 14, 30
}