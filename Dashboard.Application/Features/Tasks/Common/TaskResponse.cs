using Dashboard.Domain.Enums;
using TaskStatus = Dashboard.Domain.Enums.TaskStatus;

namespace Dashboard.Application.Features.Tasks.Common;

public class TaskDetailResponse
{
    public Guid Id { get; set; }
    public string Summary { get; set; }
    public string Key { get; set; }
    public string Description { get; set; } = string.Empty;
    public string AssigneeId { get; set; } = string.Empty;
    public string ReporterId { get; set; } = string.Empty;
    public int Estimate { get; set; } = 0;
    public int Priority { get; set; } = 0;
    public DateTime StartDate { get; set; } = DateTime.MinValue;
    public DateTime DueDate { get; set; } = DateTime.MinValue;
    public TaskStatus Status { get; set; } = TaskStatus.TODO;
    public TaskType Type { get; set; } = TaskType.Task;
    public Guid ProjectId { get; set; }
}

public class TaskResponse
{
    public Guid Id { get; set; }
    public string Summary { get; set; }
    public string Key { get; set; }
    public int Estimate { get; set; } = 0;
    public int Priority { get; set; } = 0;
    public TaskStatus Status { get; set; } = TaskStatus.TODO;
    public TaskType Type { get; set; } = TaskType.Task;
}