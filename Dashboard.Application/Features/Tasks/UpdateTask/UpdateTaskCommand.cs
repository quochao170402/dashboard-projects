using System;
using AutoMapper;
using Dashboard.Application.Features.Tasks.Common;
using Dashboard.BuildingBlock.Exceptions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.TaskDomain;
using MediatR;

namespace Dashboard.Application.Features.Tasks.UpdateTask;

public class UpdateTaskCommand(IRepository<TaskEntity> repository, IMapper mapper) : IRequestHandler<UpdateTaskRequest, TaskDetailResponse>
{
    public async Task<TaskDetailResponse> Handle(UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Task not found");

        mapper.Map(request, existing);

        var isSuccess = await repository.UpdateAsync(existing, cancellationToken);

        if (isSuccess)
        {
            throw new BusinessLogicException("Update task failed");
        }
        
        return mapper.Map<TaskDetailResponse>(existing);
    }
}
