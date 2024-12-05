using Dashboard.Domain.ProjectDomain;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.Infrastructure.DataAccess;

public class DashboardContext : DbContext
{
    public DashboardContext(DbContextOptions<DashboardContext> options) : base(options)
    {
    }

    public DbSet<Project> Projects { get; set; }
}
