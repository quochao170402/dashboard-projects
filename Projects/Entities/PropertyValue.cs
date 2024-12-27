using Projects.Common;

namespace Projects.Entities;

public class PropertyValue : Entity
{
    public Guid EntityId { get; set; }
    public string Value { get; set; }

    public virtual Guid PropertyId { get; set; }
    public virtual Property Property { get; set; }
}
