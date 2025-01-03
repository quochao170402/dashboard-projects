using Projects.Enums;

namespace Projects.Models.Settings;

public class ProjectSettingModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Label { get; set; }
    public Datatype Datatype { get; set; }
    public string Note { get; set; } = string.Empty;
    public bool IsDefault { get; set; } = false;
    public bool IsUsed { get; set; }
    public List<string> Options { get; set; } = [];
}
