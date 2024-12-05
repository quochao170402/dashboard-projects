using MediatR;

namespace Dashboard.Application.Features.Projects.DeleteProject;

public class DeleteProjectRequest : IRequest<bool>
{
    public Guid Id { get; set; }
}
