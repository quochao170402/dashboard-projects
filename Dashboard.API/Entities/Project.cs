using Dashboard.API.Entities.Common;

namespace Dashboard.API.Entities;

public class Project : Entity
{
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Key { get; set; }
    public string LogoUrl { get; set; } = string.Empty;
    public string LeaderId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}