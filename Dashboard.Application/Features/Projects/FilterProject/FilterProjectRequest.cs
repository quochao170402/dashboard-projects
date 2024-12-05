using Dashboard.Application.Features.Projects.Common;
using MediatR;

namespace Dashboard.Application.Features.Projects.FilterProject;

public class FilterProjectRequest : IRequest<(IEnumerable<ProjectResponse> projects, int count)>
{
    public string Keyword { get; set; } = string.Empty;
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
