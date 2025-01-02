using System.Reflection;
using AutoMapper;
using MediatR;
using Projects.Constants;
using Projects.Context;
using Projects.Entities;
using Projects.Enums;
using Projects.Models.Projects;
using TaskStatus = Projects.Entities.TaskStatus;

namespace Projects.Features.Projects.Version1.CreateProject;

public class CreateProjectCommand(ProjectContext context, IMapper mapper, IServiceProvider serviceProvider)
    : IRequestHandler<CreateProjectRequest, ProjectModel>
{
    public async Task<ProjectModel> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Name = request.Name,
            Key = request.Key,
            // Description = request.Description ?? string.Empty,
            // LeaderId = request.LeaderId ?? Guid.Empty,
            // Url = request.Url ?? string.Empty,
            // StartDate = request.StartDate,
            // EndDate = request.EndDate,
            // Status = ProjectStatus.NotStarted,
        };

        context.Projects.Add(project);

        var statuses = GenerateTaskStatuses(project.Id);
        context.TaskStatus.AddRange(statuses);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProjectModel>(project);
    }

    private List<TaskStatus> GenerateTaskStatuses(Guid projectId)
    {
        var defaultStatuses = typeof(TaskStatusConstants)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Select(f => f.GetValue(null)?.ToString() ?? string.Empty)
            .Where(x => !string.IsNullOrEmpty(x))
            .ToList();

        var statuses = defaultStatuses.Select(x => new TaskStatus
        {
            Name = x,
            Lable = x,
            Description = x,
            ProjectId = projectId
        }).ToList();

        return statuses;
    }

    private async Task UpdatePropertyValues(Project project)
    {
        await using var scopedContext = serviceProvider.GetRequiredService<ProjectContext>();

    }
}
