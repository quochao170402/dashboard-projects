using Dashboard.Application.Features.Projects.Common;
using Dashboard.Domain.Enums;
using MediatR;

namespace Dashboard.Application.Features.Projects.UpdateProjectStatus;

public class UpdateProjectStatusRequest : IRequest<ProjectResponse>
{
    public Guid Id { get; set; }
    public ProjectStatus Status { get; set; }
}
