namespace BilliardsManager.DTOs
{
    public class TournamentCreateDto
    {
        public DateTime Date { get; set; } = DateTime.Today;
        public List<TournamentPlacementDto> Placements { get; set; } = [];
    }
}
