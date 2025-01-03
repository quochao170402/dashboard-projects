using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Projects.Common;
using Projects.Enums;

namespace Projects.Entities;

public class Property : Entity
{
    public string Name { get; set; }
    public string Label { get; set; }
    public Datatype Datatype { get; set; }
    public string Note { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public bool IsDefault { get; set; } = false;

    public string Options { get; set; } = string.Empty;

    public virtual ICollection<PropertyValue> PropertyValues { get; set; }

    /// <summary>
    /// Init default properties
    /// </summary>
    /// <param name="name"></param>
    /// <param name="datatype"></param>
    public Property(string name, Datatype datatype)
    {
        Name = name;
        Label = name;
        IsDefault = true;
        PropertyType = PropertyType.Project;
        Datatype = datatype;
    }

    public Property()
    {

    }
}
