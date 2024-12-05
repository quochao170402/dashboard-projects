using Dashboard.Domain.Enums;

namespace Dashboard.Application.Features.Projects.Common;

public class ProjectResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Key { get; set; }
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid LeaderId { get; set; } = Guid.Empty;
    public string LeaderName { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
