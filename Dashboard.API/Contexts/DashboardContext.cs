using Dashboard.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Contexts;

public class DashboardContext : DbContext
{
    public DashboardContext(DbContextOptions<DashboardContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
}