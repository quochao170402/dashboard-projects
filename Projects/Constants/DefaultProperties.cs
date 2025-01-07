using Projects.Entities;
using Projects.Enums;

namespace Projects.Constants;

public static class DefaultProjectProperties
{
    public static Property Name = new Property("Name","Name", Datatype.Text);
    public static Property Key = new Property("Key","Key", Datatype.Text);
    public static Property Description = new Property("Description","Description", Datatype.TextArea);
    public static Property Status = new Property("Status","Status", Datatype.Number);
    public static Property Leader = new Property("Leader","Leader", Datatype.Person);
    public static Property Url = new Property("Url","Url", Datatype.Text);
    public static Property StartDate = new Property("StartDate","Start Date", Datatype.DateTime);
    public static Property EndDate = new Property("EndDate","End Date", Datatype.DateTime);
}
// public int Estimate { get; set; }
//
// public Guid AssigneeId { get; set; }
// public Guid ReporterId { get; set; }
//
// public DateTime? StartDate { get; set; }
// public DateTime? EndDate { get; set; }
//
// public TaskPriority Priority { get; set; }
// public TaskType TaskType { get; set; } = TaskType.Task;
//
// public virtual Guid ProjectId { get; set; }
// public virtual Project Project { get; set; }
//
// public virtual Guid? ParentId { get; set; }
// public virtual TaskEntity? Parent { get; set; }
//
// public virtual Guid? SprintId { get; set; }
// public virtual Sprint? Sprint { get; set; }
//
// public virtual Guid TaskStatusId { get; set; }
// public virtual TaskStatus TaskStatus { get; set; }
public static class DefaultTaskProperties
{
    public static Property Estimate = new Property("Estimate","Estimate", Datatype.Number);
    public static Property Assignee = new Property("AssigneeId","Assignee", Datatype.Person);
    public static Property Reporter = new Property("ReporterId","Reporter", Datatype.Person);

    public static Property Project = new Property("ProjectId","Project", Datatype.Text);
    public static Property Status = new Property("Status","Status", Datatype.Text);

    public static Property StartDate = new Property("StartDate","Start Date", Datatype.DateTime);
    public static Property EndDate = new Property("EndDate","End Date", Datatype.DateTime);
}
