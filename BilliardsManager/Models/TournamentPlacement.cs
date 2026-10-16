namespace BilliardsManager.Models
{
    public class TournamentPlacement
    {
        public int Id { get; set; } 
        public int TournamentId { get; set; }
        public Tournament Tournament { get; set; } = null!;

        public int PlayerId { get; set; }
        public Player? Player { get; set; } = null;

        public int Position { get; set; } 
        public string? AwardType { get; set; } // "Standard", "TopFemale"
    }

}
