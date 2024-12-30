using Microsoft.EntityFrameworkCore;
using Projects.Entities;
using TaskStatus = Projects.Entities.TaskStatus;

namespace Projects.Context;

public class ProjectContext(DbContextOptions<ProjectContext> options,  IHttpContextAccessor httpContextAccessor) : DbContext(options)
{
    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskEntity> TaskEntities { get; set; }
    public DbSet<Property> Properties { get; set; }
    public DbSet<PropertyValue> PropertyValues { get; set; }
    public DbSet<Sprint> Sprints { get; set; }
    public DbSet<PropertySetting> PropertySettings { get; set; }
    public DbSet<TaskStatus> TaskStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // TaskEntity to TaskStatus relationship (Many-to-One)
        modelBuilder.Entity<TaskEntity>()
            .HasOne(t => t.TaskStatus)  // Task has one TaskStatus
            .WithMany()  // TaskStatus can be used by many tasks (no reverse navigation)
            .HasForeignKey(t => t.TaskStatusId)
            .OnDelete(DeleteBehavior.Restrict);  // Prevent TaskStatus deletion if Tasks exist

    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        // Run logic before saving changes
        BeforeSaveChange();

        // Save changes to database
        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    private void BeforeSaveChange()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted)
            .ToList();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                    entry.Property("LatestUpdatedAt").CurrentValue = DateTime.Now;
                    entry.Property("CreatedBy").CurrentValue = Guid.Empty;
                    entry.Property("LatestUpdatedBy").CurrentValue = Guid.Empty;
                    break;
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.Property("LatestUpdatedAt").CurrentValue = DateTime.Now;
                    entry.Property("LatestUpdatedBy").CurrentValue = Guid.Empty;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                default:
                    break;
            }

            // var userId = httpContextAccessor.HttpContext?.User?.Identity?.Name;

            var entityId = entry.Property("Id").CurrentValue?.ToString();
        }
    }
}
