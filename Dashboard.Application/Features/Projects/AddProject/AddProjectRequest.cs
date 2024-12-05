using MediatR;

namespace Dashboard.Application.Features.Projects.AddProject;

public class AddProjectRequest : IRequest<bool>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Key { get; set; }
}
