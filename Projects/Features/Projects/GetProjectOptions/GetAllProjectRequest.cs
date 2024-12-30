using MediatR;
using Projects.Common;

namespace Projects.Features.Projects.GetProjectOptions;

public class GetProjectOptionRequest : IRequest<List<OptionModel>>
{

}
