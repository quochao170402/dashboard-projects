using Projects.Features.Settings.AddProperty;

namespace Projects.Features.Settings.UpdateProperty;

public class UpdatePropertyRequest : AddPropertyRequest
{
    public Guid Id { get; set; }
}
