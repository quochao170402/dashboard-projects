namespace Dashboard.API.Payload;

public class UpdateProject
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
