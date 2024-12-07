using Dashboard.Application.Features.Tasks.Common;
using MediatR;

namespace Dashboard.Application.Features.Tasks.CreateTask;

public class CreateTaskRequest : IRequest<TaskResponse>
{
    public Guid ProjectId { get; set; }
    public string Summary { get; set; }
}
