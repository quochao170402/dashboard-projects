namespace Dashboard.API.Entities.Common;

public interface IEntity
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime LatestUpdatedAt { get; set; }
    public Guid LatestUpdatedBy { get; set; }
}
