using MediatR;
using Projects.Models.Projects;

namespace Projects.Features.Projects.Version1.FilterProjects;

public class FilterProjects : IRequest<(List<ProjectModel> projects, int count)>
{
    public int PageSize { get; set; }

    public int PageIndex { get; set; }
}
