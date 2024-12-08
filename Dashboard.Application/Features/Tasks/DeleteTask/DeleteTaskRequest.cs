using System;
using MediatR;

namespace Dashboard.Application.Features.Tasks.DeleteTask;

public class DeleteTaskRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}
