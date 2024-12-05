using MediatR;

namespace Dashboard.Application.Features.Projects.UpdateProject;

public class UpdateProjectRequest : IRequest<bool>
{
    public Guid ExistingId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
