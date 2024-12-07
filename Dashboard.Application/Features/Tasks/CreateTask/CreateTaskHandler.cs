using Dashboard.Application.Features.Tasks.Common;
using Dashboard.BuildingBlock.Exceptions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.ProjectDomain;
using Dashboard.Domain.TaskDomain;
using MediatR;

namespace Dashboard.Application.Features.Tasks.CreateTask;

public class CreateTaskHandler(
    IRepository<Project> projectRepository,
    ITaskRepository taskRepository)
    : IRequestHandler<CreateTaskRequest, TaskResponse>
{
    public async Task<TaskResponse> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
                      ?? throw new EntityNotFoundException("Project not found");

        var order = await taskRepository.CountByProjectId(project.Id, cancellationToken);
        var key = $"{project.Key}-{order + 1}";
        var task = new TaskEntity
        {
            Summary = request.Summary,
            Key = key,
            ProjectId = request.ProjectId
        };

        throw new NotImplementedException();
    }
}
