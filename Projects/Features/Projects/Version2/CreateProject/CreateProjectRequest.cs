using MediatR;
using Projects.Models.Projects;

namespace Projects.Features.Projects.Version2.CreateProject;

public class CreateProjectRequest : IRequest<bool>
{
    public List<ProjectPropertyRequest> Properties { get; set; } = [];
}
