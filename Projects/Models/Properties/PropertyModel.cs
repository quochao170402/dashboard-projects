using Projects.Entities;
using Projects.Enums;

namespace Projects.Models.Properties;

public class PropertyModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public Datatype Datatype { get; set; }
    public string Note { get; set; }
}
