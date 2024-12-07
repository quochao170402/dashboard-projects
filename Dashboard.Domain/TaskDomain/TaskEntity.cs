using Dashboard.Domain.Common;
using Dashboard.Domain.Enums;
using TaskStatus = Dashboard.Domain.Enums.TaskStatus;

namespace Dashboard.Domain.TaskDomain;

public class TaskEntity : Entity, IAggregateRoot
{
    public string Summary { get; set; }
    public string Key { get; set; }
    public string Description { get; set; }
    public string AssigneeId { get; set; }
    public string ReporterId { get; set; }
    public int Priority { get; set; }
    public DateTime DueDate { get; set; }
    public TaskStatus Status { get; set; }
    public TaskType Type { get; set; }
    public Guid ProjectId { get; set; }
}
