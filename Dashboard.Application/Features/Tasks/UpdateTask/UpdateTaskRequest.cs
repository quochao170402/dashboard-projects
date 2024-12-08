using System;
using Dashboard.Application.Features.Tasks.Common;
using Dashboard.Application.Features.Tasks.CreateTask;
using MediatR;

namespace Dashboard.Application.Features.Tasks.UpdateTask;

public class UpdateTaskRequest : CreateTaskRequest
{
    public Guid Id { get; set; }
}
