using Dashboard.Application.Features.Projects.Common;
using Dashboard.BuildingBlock.Exceptions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.ProjectDomain;
using MediatR;

namespace Dashboard.Application.Features.Projects.UpdateProjectStatus;

public class UpdateProjectStatusCommand(IRepository<Project> repository)
    : IRequestHandler<UpdateProjectStatusRequest, ProjectResponse>
{
    public async Task<ProjectResponse> Handle(UpdateProjectStatusRequest request, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(request.Id, cancellationToken)
                      ?? throw new EntityNotFoundException("Project not found");
        project.UpdateStatus(request.Status);
        await repository.UpdateAsync(project, cancellationToken);
        return new ProjectResponse
        {
            Id = project.Id,
            Name = project.Name,
            Key = project.Key,
            Description = project.Description,
            Status = project.Status,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            LeaderId = project.LeaderId,
            Url = project.Url
        };
    }
}
