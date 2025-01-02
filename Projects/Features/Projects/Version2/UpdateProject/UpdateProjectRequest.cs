using Projects.Features.Projects.Version2.CreateProject;

namespace Projects.Features.Projects.Version2.UpdateProject;

public class UpdateProjectRequest : CreateProjectRequest
{
    public Guid Id { get; set; }
}
