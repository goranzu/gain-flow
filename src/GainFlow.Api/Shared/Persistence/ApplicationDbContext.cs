using GainFlow.Api.Shared.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GainFlow.Api.Shared.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Exercise> Exercises { get; set; }
    public DbSet<WorkoutProgram> WorkoutPrograms { get; set; }
    public DbSet<ProgramWeek> ProgramWeeks { get; set; }
    public DbSet<ProgramDay> ProgramDays { get; set; }
    public DbSet<ProgramSetGroup> ProgramSetGroups { get; set; }
    public DbSet<ProgramExercise> ProgramExercises { get; set; }
    public DbSet<UserProgram> UserPrograms { get; set; }
    public DbSet<WorkoutSession> WorkoutSessions { get; set; }
    public DbSet<WorkoutSet> WorkoutSets { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        foreach (EntityEntry<AuditableEntity> entity in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    entity.Entity.CreatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entity.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
            }
        }


        return base.SaveChangesAsync(cancellationToken);
    }
}
