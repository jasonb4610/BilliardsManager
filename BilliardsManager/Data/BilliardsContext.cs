using BilliardsManager.Models;
using Microsoft.EntityFrameworkCore;

public class BilliardsContext : DbContext
{
    public DbSet<Player> Players => Set<Player>();
    public DbSet<BreakPotSession> BreakPotSessions => Set<BreakPotSession>();
    public DbSet<TicketPurchase> TicketPurchases => Set<TicketPurchase>();
    public DbSet<Tournament> Tournaments => Set<Tournament>();

    public BilliardsContext(DbContextOptions<BilliardsContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure decimal precision for currency fields
        modelBuilder.Entity<BreakPotSession>()
            .Property(b => b.StartingPot).HasPrecision(18, 2);
        modelBuilder.Entity<BreakPotSession>()
            .Property(b => b.TicketSalesRevenue).HasPrecision(18, 2);
        modelBuilder.Entity<BreakPotSession>()
            .Property(b => b.PayoutAmount).HasPrecision(18, 2);
        modelBuilder.Entity<BreakPotSession>()
            .Property(b => b.EndingPot).HasPrecision(18, 2);
        modelBuilder.Entity<TicketPurchase>()
            .Property(t => t.TierAmount).HasPrecision(18, 2);
    }
}