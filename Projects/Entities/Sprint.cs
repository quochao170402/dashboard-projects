using Projects.Common;

namespace Projects.Entities;

public class Sprint : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public virtual Guid ProjectId { get; set; }
    public virtual Project Project { get; set; }

    public virtual ICollection<TaskEntity> TaskEntities { get; set; }
}
