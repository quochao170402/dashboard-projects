using System;
using AutoMapper;
using Dashboard.Application.Features.Common;
using Dashboard.Application.Features.Tasks.Common;
using Dashboard.Domain.TaskDomain;
using MediatR;

namespace Dashboard.Application.Features.Tasks.FilterTask;

public class FilterTaskQuery(ITaskRepository taskRepository,
IMapper mapper) : IRequestHandler<FilterTaskRequest, FilterResponse>
{
    public async Task<FilterResponse> Handle(FilterTaskRequest request, CancellationToken cancellationToken)
    {
        var tasks = await taskRepository.FilterAsync(request.ProjectId, request.Keyword, request.PageSize, request.PageIndex, cancellationToken);
        var count = await taskRepository.CountByConditionAsync(request.ProjectId, request.Keyword, cancellationToken);
        var response = mapper.Map<List<TaskEntity>, List<TaskDetailResponse>>(tasks);
        return new FilterResponse
        {
            Data = response,
            Count = count
        };
    }
}
