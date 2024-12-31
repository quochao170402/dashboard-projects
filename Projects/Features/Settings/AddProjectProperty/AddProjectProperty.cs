using MediatR;
using Projects.Enums;

namespace Projects.Features.Settings.AddProjectProperty;

public class AddProjectProperty : IRequest<bool>
{
    public string Name { get; set; }
    public Datatype Datatype { get; set; }
}
