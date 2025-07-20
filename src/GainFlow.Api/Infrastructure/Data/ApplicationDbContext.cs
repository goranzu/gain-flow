using GainFlow.Api.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GainFlow.Api.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Exercise> Exercises { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
