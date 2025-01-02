using MediatR;
using Projects.Common;

namespace Projects.Features.Projects.Version1.GetProjectOptions;

public class GetProjectOptionRequest : IRequest<List<OptionModel>>
{

}
