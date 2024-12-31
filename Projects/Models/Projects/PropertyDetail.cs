using Projects.Enums;

namespace Projects.Models.Projects;

public class PropertyDetail
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public Datatype Datatype { get; set; }
    public bool IsDefault { get; set; } = false;
    public string Value { get; set; }
    public bool IsUsed { get; set; } = false;
}
