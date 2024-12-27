using MediatR;
using Projects.Common;

namespace Projects.Features.Projects.GetAllProjects;

public class GetProjectOptionRequest : IRequest<List<OptionModel>>
{

}
