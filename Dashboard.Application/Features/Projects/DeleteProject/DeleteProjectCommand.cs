using Dashboard.BuildingBlock.Exceptions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.Enums;
using Dashboard.Domain.ProjectDomain;
using MediatR;

namespace Dashboard.Application.Features.Projects.DeleteProject;

public class DeleteProjectCommand(IRepository<Project> repository) : IRequestHandler<DeleteProjectRequest, bool>
{
    public async Task<bool> Handle(DeleteProjectRequest request, CancellationToken cancellationToken)
    {
        var project = await repository.GetByIdAsync(request.Id, cancellationToken)
                      ?? throw new EntityNotFoundException("Project not found");

        if (project.Status == ProjectStatus.Completed || project.Status == ProjectStatus.Cancelled)
        {
            throw new InvalidProjectException("Can't delete completed project");
        }

        var result = await repository.DeleteAsync(project, cancellationToken);
        return result;
    }
}
