using Projects.Common;
using Projects.Enums;

namespace Projects.Entities;

public class Property : Entity
{
    public string Name { get; set; }
    public string Label { get; set; }
    public Datatype Datatype { get; set; }
    public string Note { get; set; }
    public PropertyType PropertyType { get; set; }
    public virtual ICollection<PropertyValue> PropertyValues { get; set; }
}
