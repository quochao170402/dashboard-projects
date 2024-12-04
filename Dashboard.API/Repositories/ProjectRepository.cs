using Dashboard.API.Contexts;
using Dashboard.API.Entities;
using Dashboard.API.Repositories.Common;

namespace Dashboard.API.Repositories;

public interface IProjectRepository : IRepository<Project>
{
}

public class ProjectRepository : Repository<Project>, IProjectRepository
{
    public ProjectRepository(DashboardContext context) : base(context)
    {
    }
}