using Dashboard.Domain.Common;
using Dashboard.Domain.Enums;

namespace Dashboard.Domain.ProjectDomain;

public class Project : Entity, IAggregateRoot
{
    public Project()
    {
    }

    public Project(string name, string key, string description, DateTime startDate, DateTime endDate)
    {
        Name = name;
        Key = key;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
    }

    public DateTime StartDate { get; internal set; }
    public DateTime EndDate { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; } = string.Empty;
    public ProjectStatus Status { get; private set; } = ProjectStatus.NotStarted;
    public string Key { get; private set; }
    public Guid LeaderId { get; private set; } = Guid.Empty;
    public string Url { get; set; } = string.Empty;

    public void UpdateStatus(ProjectStatus status)
    {
        switch (status)
        {
            case ProjectStatus.NotStarted:
                if (Status == ProjectStatus.Completed)
                    throw new ArgumentException("Project status cannot be completed.");
                break;
            case ProjectStatus.InProgress:
                break;
            case ProjectStatus.Completed:
            case ProjectStatus.Cancelled:
                EndDate = DateTime.Now;
                break;
        }

        Status = status;
    }

    public void AssignLeader(Guid leaderId)
    {
        LeaderId = leaderId;
    }

    public void SetUrl(string url)
    {
        Url = url;
    }

    public void Update(Project entity)
    {
        Name = entity.Name;
        Description = entity.Description;
        StartDate = entity.StartDate;
        EndDate = entity.EndDate;
        Status = entity.Status;
        Url = entity.Url;
        LeaderId = entity.LeaderId;
    }
}
