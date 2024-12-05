using Dashboard.BuildingBlock.Exceptions;
using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.Enums;
using Dashboard.Domain.ProjectDomain;
using MediatR;

namespace Dashboard.Application.Features.Projects.UpdateProject;

public class UpdateProjectCommand(IRepository<Project> repository, IProjectRepository projectRepository)
    : IRequestHandler<UpdateProjectRequest, bool>
{
    public async Task<bool> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var existing = await repository.GetByIdAsync(request.ExistingId, cancellationToken)
                       ?? throw new EntityNotFoundException("Project not found");

        await Validate(existing, request, cancellationToken);

        existing.Update(
            new Project(request.Name, string.Empty, request.Description, request.StartDate, request.EndDate)
            {
                Url = request.Url
            });

        return await repository.UpdateAsync(existing, cancellationToken);
    }

    private async Task Validate(Project existing, UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        if (existing.Status == ProjectStatus.Completed)
            throw new InvalidProjectException("Can't update completed project");

        if (await projectRepository.IsNameExist(request.Name, existing.Id, cancellationToken))
            throw new InvalidProjectException("Project name was existed");
    }
}
