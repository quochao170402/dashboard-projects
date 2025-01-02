using MediatR;

namespace Projects.Features.Projects.Version2.UpdateProject;

public class UpdateProjectCommand : IRequestHandler<UpdateProjectRequest, bool>
{
    public async Task<bool> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
