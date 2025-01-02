using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Entities;
using Projects.Models.Projects;

namespace Projects.Features.Projects.Version1.FilterProjects;

public class FilterProjectsQuery(ProjectContext context, IMapper mapper)
    : IRequestHandler<FilterProjects, (List<ProjectModel> projects, int count)>
{
    public async Task<(List<ProjectModel> projects, int count)> Handle(FilterProjects request,
        CancellationToken cancellationToken)
    {
        var count = await context.Projects.CountAsync(cancellationToken);

        var projects = await context.Projects.OrderBy(x => x.CreatedAt)
            .AsNoTracking()
            .Skip(request.PageSize * (request.PageIndex - 1))
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return (projects: mapper.Map<List<Project>, List<ProjectModel>>(projects), count);
    }
}
