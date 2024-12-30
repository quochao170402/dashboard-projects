using MediatR;
using Projects.Enums;
using Projects.Models.Properties;

namespace Projects.Features.Settings.GetProperties;

public class GetProperties : IRequest<List<PropertyModel>>
{
    public PropertyType Type { get; set; } = PropertyType.Project;
}
