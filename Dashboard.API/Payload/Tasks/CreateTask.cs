namespace Dashboard.API.Payload.Tasks;

public class CreateTask
{
    public Guid ProjectId { get; set; }
    public string Summary { get; set; }
}
