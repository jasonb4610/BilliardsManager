using BilliardsManager.DTOs;
using BilliardsManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class TournamentsController : ControllerBase
{
    private readonly BilliardsContext _context;

    public TournamentsController(BilliardsContext context)
    {
        _context = context;
    }

    // GET: api/tournaments
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tournament>>> GetTournaments()
    {
        return await _context.Tournaments
            .Include(t => t.Placements)
                .ThenInclude(p => p.Player)
            .OrderByDescending(t => t.Date)
            .ToListAsync();
    }

    // GET: api/tournaments/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Tournament>> GetTournament(int id)
    {
        var tournament = await _context.Tournaments
            .Include(t => t.Placements)
                .ThenInclude(p => p.Player)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tournament == null)
        {
            return NotFound();
        }

        return Ok(tournament);
    }

    // POST: api/tournaments
    [HttpPost]
    public async Task<ActionResult<Tournament>> CreateTournament([FromBody] TournamentCreateDto dto)
    {
        if (dto.Placements == null || dto.Placements.Count == 0)
        {
            return BadRequest("A tournament must have at least one placement result.");
        }

        var tournament = new Tournament
        {
            Date = dto.Date,
            Placements = dto.Placements.Select(p => new TournamentPlacement
            {
                PlayerId = p.PlayerId,
                Position = p.Position,
                AwardType = string.IsNullOrWhiteSpace(p.AwardType) ? "Standard" : p.AwardType
            }).ToList()
        };

        _context.Tournaments.Add(tournament);
        await _context.SaveChangesAsync();

        // Reload with player details populated for the HTTP response
        var createdTournament = await _context.Tournaments
            .Include(t => t.Placements)
                .ThenInclude(p => p.Player)
            .FirstOrDefaultAsync(t => t.Id == tournament.Id);

        return CreatedAtAction(nameof(GetTournament), new { id = tournament.Id }, createdTournament);
    }

    // DELETE: api/tournaments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTournament(int id)
    {
        var tournament = await _context.Tournaments.FindAsync(id);
        if (tournament == null)
        {
            return NotFound();
        }

        _context.Tournaments.Remove(tournament);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}