using System;
using Dashboard.BuildingBlock.Exceptions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.TaskDomain;
using MediatR;

namespace Dashboard.Application.Features.Tasks.DeleteTask;

public class DeleteTaskCommand(IRepository<TaskEntity> taskEntityRepository) : IRequestHandler<DeleteTaskRequest, bool>
{

    public async Task<bool> Handle(DeleteTaskRequest request, CancellationToken cancellationToken)
    {
        var existing = await taskEntityRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new EntityNotFoundException("Task not found");

        var isSuccess = await taskEntityRepository.DeleteAsync(existing, cancellationToken);
        if (!isSuccess)
        {
            throw new BusinessLogicException("Delete task failed");
        }

        return isSuccess;
    }
}
