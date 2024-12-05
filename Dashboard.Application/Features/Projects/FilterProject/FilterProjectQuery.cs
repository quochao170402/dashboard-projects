using Dashboard.Application.Features.Projects.Common;
using Dashboard.Domain.ProjectDomain;
using MediatR;

namespace Dashboard.Application.Features.Projects.FilterProject;

public class
    FilterProjectQuery(IProjectRepository projectRepository, IServiceProvider serviceProvider)
    : IRequestHandler<FilterProjectRequest, (IEnumerable<ProjectResponse> projects, int count)>
{
    public async Task<(IEnumerable<ProjectResponse> projects, int count)> Handle(FilterProjectRequest request,
        CancellationToken cancellationToken)
    {
        var filterResult = await projectRepository.FilterAsync(
            request.Keyword, request.PageSize, request.PageIndex, cancellationToken);

        return (filterResult.Projects.Select(x => new ProjectResponse
        {
            Id = x.Id,
            Name = x.Name,
            Key = x.Key,
            Description = x.Description,
            Status = x.Status,
            StartDate = x.StartDate,
            EndDate = x.EndDate,
            LeaderId = x.LeaderId,
            Url = x.Url
        }), count: filterResult.Count);
    }
}
