using Dashboard.Application.Features.Projects.Common;
using MediatR;

namespace Dashboard.Application.Features.Projects.GetProjectById;

public class GetProjectByIdRequest : IRequest<ProjectResponse>
{
    public Guid Id { get; set; }
}
