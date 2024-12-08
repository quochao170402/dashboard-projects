using Dashboard.Domain.ProjectDomain;
using Dashboard.Domain.TaskDomain;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.DataAccess;

public class DashboardContext(DbContextOptions<DashboardContext> options) : DbContext(options)
{

    public DbSet<Project> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSeeding((context, _) =>
                {
                    context.Set<Project>().AddRange(new List<Project>(){
                        new Project("Project 1", "P01", "", DateTime.Today, DateTime.Today.AddDays(10)),
                        new Project("Project 2", "P02", "", DateTime.Today, DateTime.Today.AddDays(10)),
                        new Project("Project 3", "P03", "", DateTime.Today, DateTime.Today.AddDays(10)),
                    });
                    context.SaveChanges();
                });
        base.OnConfiguring(optionsBuilder);
    }
}
