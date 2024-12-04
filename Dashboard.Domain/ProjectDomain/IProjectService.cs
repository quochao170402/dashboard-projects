namespace Dashboard.Domain.ProjectDomain;

public interface IProjectService
{
    Task<Project> UpdateProject(string id, Project project);
    Task<Project> AssignLeader(string id, string leaderId);
}
