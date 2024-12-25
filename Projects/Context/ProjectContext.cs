using Microsoft.EntityFrameworkCore;
using Projects.Entities;

namespace Projects.Context;

public class ProjectContext : DbContext
{
    public ProjectContext(DbContextOptions<ProjectContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
}
