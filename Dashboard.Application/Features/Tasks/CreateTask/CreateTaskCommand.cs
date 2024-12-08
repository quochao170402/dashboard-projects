using AutoMapper;
using Dashboard.Application.Features.Tasks.Common;
using Dashboard.BuildingBlock.Exceptions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.ProjectDomain;
using Dashboard.Domain.TaskDomain;
using MediatR;

namespace Dashboard.Application.Features.Tasks.CreateTask;

public class CreateTaskCommand(
    IRepository<Project> projectRepository,
    IRepository<TaskEntity> taskEntityRepository,
    ITaskRepository taskRepository,
    IMapper mapper)
    : IRequestHandler<CreateTaskRequest, TaskDetailResponse>
{
    public async Task<TaskDetailResponse> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var project = await projectRepository.GetByIdAsync(request.ProjectId, cancellationToken)
                      ?? throw new EntityNotFoundException("Project not found");

        var order = await taskRepository.CountByProjectId(project.Id, cancellationToken);
        var key = $"{project.Key}-{order + 1}";

        var task = mapper.Map<CreateTaskRequest, TaskEntity>(request);
        task.Key = key;

        var isSuccess = await taskEntityRepository.AddAsync(task, cancellationToken);

        if (!isSuccess)
        {
            throw new BusinessLogicException("Create task failed");
        }

        return mapper.Map<TaskEntity, TaskDetailResponse>(task);
    }
}
