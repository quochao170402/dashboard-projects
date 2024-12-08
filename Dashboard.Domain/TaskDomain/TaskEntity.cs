using Dashboard.Domain.Common;
using Dashboard.Domain.Enums;
using TaskStatus = Dashboard.Domain.Enums.TaskStatus;

namespace Dashboard.Domain.TaskDomain;

public class TaskEntity : Entity, IAggregateRoot
{
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
