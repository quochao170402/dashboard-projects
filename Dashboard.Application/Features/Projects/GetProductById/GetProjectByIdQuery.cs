using Dashboard.Application.Features.Projects.Common;
using Dashboard.BuildingBlock.Exceptions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.ProjectDomain;
using MediatR;

namespace Dashboard.Application.Features.Projects.GetProductById;

public class GetProjectByIdQuery(IRepository<Project> repository)
    : IRequestHandler<GetProjectByIdRequest, ProjectResponse>
{
    public async Task<ProjectResponse> Handle(GetProjectByIdRequest request, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(request.Id, cancellationToken)
                      ?? throw new EntityNotFoundException("Project not found");

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
