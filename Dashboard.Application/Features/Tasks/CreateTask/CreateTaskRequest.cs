using System.ComponentModel.DataAnnotations;
using Dashboard.Application.Features.Tasks.Common;
using Dashboard.Domain.Enums;
using MediatR;

namespace Dashboard.Application.Features.Tasks.CreateTask;

public class CreateTaskRequest : IRequest<TaskDetailResponse>
{
    [Required]
    public Guid ProjectId { get; set; }

    [Required]
    public string Summary { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AssigneeId { get; set; } = string.Empty;
    public string ReporterId { get; set; } = string.Empty;
    public int Estimate { get; set; } = 0;
    public int Priority { get; set; } = 0;
    public DateTime StartDate { get; set; } = DateTime.MinValue;
    public DateTime DueDate { get; set; } = DateTime.MinValue;
    public Domain.Enums.TaskStatus Status { get; set; } = Domain.Enums.TaskStatus.TODO;
    public TaskType Type { get; set; } = TaskType.Task;
}
