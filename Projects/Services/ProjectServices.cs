using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Controllers.Payload;
using Projects.Entities;
using Projects.Exceptions;
using Projects.Models;

namespace Projects.Services;

public interface IProjectService
{
    Task<List<ProjectModel>> GetAll();
    Task<(List<ProjectModel> projects, int count)> Get(string? keyword, int pageSize, int pageIndex);
    Task<ProjectModel> GetById(Guid id);
    Task<List<ProjectModel>> GetByUserId(Guid userId);
    Task<ProjectModel> Add(AddProjectRequest request);
    Task<ProjectModel> Update(Guid projectId, UpdateProjectRequest request);
    Task<bool> Delete(Guid projectId);
}

public class ProjectService(ProjectContext context, IMapper mapper) : IProjectService
{
    public async Task<List<ProjectModel>> GetAll()
    {
        var projects = await context.Projects.AsNoTracking().ToListAsync();
        return mapper.Map<List<Project>, List<ProjectModel>>(projects);
    }

    public async Task<(List<ProjectModel> projects, int count)> Get(string? keyword, int pageSize, int pageIndex)
    {
        var query = CreateFilterQuery(keyword);
        var projects = await query
            .OrderBy(x => x.CreatedAt)
            .Skip(pageSize * (pageIndex - 1))
            .Take(pageSize)
            .ToListAsync();

        var count = await query.CountAsync();

        return (
            projects: mapper.Map<List<Project>, List<ProjectModel>>(projects),
            count
        );
    }

    public async Task<ProjectModel> GetById(Guid id)
    {
        var project = await context.Projects.FindAsync(id)
                      ?? throw new EntityNotFoundException("Not found project");

        return mapper.Map<Project, ProjectModel>(project);
    }

    public async Task<List<ProjectModel>> GetByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<ProjectModel> Add(AddProjectRequest request)
    {
        var project = mapper.Map<AddProjectRequest, Project>(request);
        context.Projects.Add(project);
        await context.SaveChangesAsync();
        return mapper.Map<Project, ProjectModel>(project);
    }

    public async Task<ProjectModel> Update(Guid projectId, UpdateProjectRequest request)
    {
        var project = await context.Projects.FindAsync(projectId)
                      ?? throw new EntityNotFoundException("Not found project");
        mapper.Map(request, project);
        context.Projects.Update(project);
        await context.SaveChangesAsync();
        return mapper.Map<Project, ProjectModel>(project);
    }

    public async Task<bool> Delete(Guid projectId)
    {
        var project = await context.Projects.FindAsync(projectId)
                      ?? throw new EntityNotFoundException("Not found project");
        project.IsDeleted = true;
        context.Projects.Update(project);
        return await context.SaveChangesAsync() != 0;
    }

    #region Private methods

    private IQueryable<Project> CreateFilterQuery(string? keyword)
    {
        var query = context.Projects.AsQueryable();
        if (!string.IsNullOrEmpty(keyword)) query = query.Where(x => x.Name.Contains(keyword));

        return query;
    }

    #endregion
}
