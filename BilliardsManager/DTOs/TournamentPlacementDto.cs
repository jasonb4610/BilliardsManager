namespace BilliardsManager.DTOs
{
    public class TournamentPlacementDto
    {
        public int PlayerId { get; set; } 
        public int Position { get; set; }
        public string? AwardType { get; set; } = "Standard";
    }
}
