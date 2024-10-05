using Microsoft.EntityFrameworkCore;
using PointsApp.Models;

namespace PointsApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<PointTransaction> PointTransactions { get; set; } = null!;
}