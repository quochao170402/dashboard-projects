using MediatR;
using Projects.Models.Projects;

namespace Projects.Features.Projects.Version1.GetProjectsPaging;

public class GetProjectsPaging : IRequest<(List<ProjectDetailModel> projects, int count)>
{
    public int PageSize { get; set; }

    public int PageIndex { get; set; }
}
