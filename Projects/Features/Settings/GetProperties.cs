using MediatR;
using Projects.Enums;
using Projects.Models.Properties;

namespace Projects.Features.Settings;

public class GetProperties : IRequest<List<PropertyModel>>
{
    public PropertyType? Type { get; set; }
}
