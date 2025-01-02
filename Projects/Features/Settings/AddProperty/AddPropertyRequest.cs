using MediatR;
using Projects.Enums;

namespace Projects.Features.Settings.AddProperty;

public class AddPropertyRequest : IRequest<bool>
{
    public string Name { get; set; }
    public string Label { get; set; }
    public Datatype Datatype { get; set; }
    public string Note { get; set; } = string.Empty;
    public PropertyType PropertyType { get; set; }
    public bool IsDefault { get; set; } = false;
    public bool IsUsed { get; set; }
}
