using Projects.Common;
using Projects.Enums;

namespace Projects.Entities;

public class TaskEntity : Entity
{
    public string Summary { get; set; }
    public string Description { get; set; }

    public int Estimate { get; set; }

    public Guid AssigneeId { get; set; }
    public Guid ReporterId { get; set; }

    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public TaskPriority Priority { get; set; }
    public TaskType TaskType { get; set; } = TaskType.Task;

    public virtual Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }

    public virtual Guid? ParentId { get; set; }
    public virtual TaskEntity? Parent { get; set; }

    public virtual Guid? SprintId { get; set; }
    public virtual Sprint? Sprint { get; set; }

    public virtual Guid TaskStatusId { get; set; }
    public virtual TaskStatus TaskStatus { get; set; }
}
