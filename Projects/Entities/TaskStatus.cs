using Projects.Common;

namespace Projects.Entities;

public class TaskStatus : Entity
{
    public string Name { get; set; }
    public string Lable { get; set; }
    public string Description { get; set; }

    public virtual Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }
}
