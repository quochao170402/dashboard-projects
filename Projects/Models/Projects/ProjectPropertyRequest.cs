using MediatR;

namespace Projects.Models.Projects;

public class ProjectPropertyRequest
{
    public Guid PropertyId { get; set; }
    public string Value { get; set; }
}
