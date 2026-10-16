using BilliardsManager.Models;

public class Tournament
{
    public int Id { get; set; }
    public DateTime Date { get; set; } = DateTime.Today;

    // One-to-many relation for flexible payouts
    public ICollection<TournamentPlacement> Placements { get; set; } = [];


}