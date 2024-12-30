using MediatR;
using Projects.Models;
using Projects.Models.Projects;

namespace Projects.Features.Projects.CreateProject;

public class CreateProjectRequest : IRequest<ProjectModel>
{
    public string Name { get; set; }
    public string Key { get; set; }
    public string? Description { get; set; } = string.Empty;
    public Guid? LeaderId { get; set; } = Guid.Empty;
    public string? Url { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.MinValue;
    public DateTime EndDate { get; set; } = DateTime.MinValue;
}
