using Dashboard.BuildingBlock.Repository;
using Dashboard.Domain.ProjectDomain;
using MediatR;

namespace Dashboard.Application.Features.Projects.AddProject;

public class AddProjectCommand(IProjectRepository projectRepository, IRepository<Project> repository)
    : IRequestHandler<AddProjectRequest, bool>
{
    public async Task<bool> Handle(AddProjectRequest request, CancellationToken cancellationToken)
    {
        await Validate(request, cancellationToken);

        var isSuccess = await repository.AddAsync(
            new Project(request.Name, request.Key, request.Description, request.StartDate, request.EndDate)
            {
                Url = request.Url
            },
            cancellationToken);

        return isSuccess;
    }

    private async Task Validate(AddProjectRequest request, CancellationToken cancellationToken)
    {
        if (await projectRepository.IsNameExist(request.Name, Guid.Empty, cancellationToken))
            throw new InvalidProjectException("Project name was existed");

        if (await projectRepository.IsKeyExist(request.Key, Guid.Empty, cancellationToken))
            throw new InvalidProjectException("Project key was existed");
    }
}
