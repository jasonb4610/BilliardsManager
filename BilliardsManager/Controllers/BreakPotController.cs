using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BilliardsManager.DTOs;

[Route("api/[controller]")]
[ApiController]
public class BreakPotController : ControllerBase
{
    private readonly BilliardsContext _context;

    public BreakPotController(BilliardsContext context)
    {
        _context = context;
    }

    // Get active or latest session
    [HttpGet("latest")]
    public async Task<ActionResult<BreakPotSession>> GetLatestSession()
    {
        var session = await _context.BreakPotSessions
            .Include(b => b.DrawerPlayer)
            .Include(b => b.TicketPurchases)
            .ThenInclude(tp => tp.Player)
            .OrderByDescending(b => b.Date)
            .FirstOrDefaultAsync();

        if (session == null)
        {
            // Return a default template if none exist yet (Week 1 starting pot)
            return Ok(new BreakPotSession { StartingPot = 0, Date = DateTime.Today });
        }

        return Ok(session);
    }

    // Add a ticket purchase to a session
    [HttpPost("{sessionId}/tickets")]
    public async Task<ActionResult<TicketPurchase>> AddTickets(int sessionId, TicketPurchase purchase)
    {
        purchase.BreakPotSessionId = sessionId;
        _context.TicketPurchases.Add(purchase);

        // Update session revenue
        var session = await _context.BreakPotSessions.FindAsync(sessionId);
        if (session != null)
        {
            session.TicketSalesRevenue += (purchase.TierAmount); // or TierAmount * TicketCount depending on your pricing model
        }

        await _context.SaveChangesAsync();
        return Ok(purchase);
    }

    // Complete the break attempt and calculate payout
    [HttpPost("{sessionId}/complete-break")]
    public async Task<IActionResult> CompleteBreak(int sessionId, [FromBody] BreakCompletionDto dto)
    {
        var session = await _context.BreakPotSessions
            .Include(b => b.TicketPurchases)
            .FirstOrDefaultAsync(b => b.Id == sessionId);

        if (session == null) return NotFound();

        session.DrawerPlayerId = dto.DrawerPlayerId;
        session.BallsMade = dto.BallsMade;

        // Math: Payout = (TotalPot / 10) * BallsMade
        decimal perBallValue = session.TotalPot / 10m;
        session.PayoutAmount = perBallValue * dto.BallsMade;
        session.EndingPot = session.TotalPot - session.PayoutAmount;

        await _context.SaveChangesAsync();
        return Ok(session);
    }
}