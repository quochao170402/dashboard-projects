using Projects.Common;
using Projects.Enums;

namespace Projects.Entities;

public class PropertySetting : Entity
{
    public bool IsUsed { get; set; }
    public PropertyType Type { get; set; }
    public virtual Guid PropertyId { get; set; }
    public virtual Property Property { get; set; }
}
