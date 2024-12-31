using MediatR;
using Projects.Models;
using Projects.Models.Projects;

namespace Projects.Features.Projects.GetProjectsPaging;

[Obsolete("Replace by filter projects")]
public class GetProjectsPaging : IRequest<(List<ProjectDetailModel> projects, int count)>
{
    public int PageSize { get; set; }

    public int PageIndex { get; set; }
}
