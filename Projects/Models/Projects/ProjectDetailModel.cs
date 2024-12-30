using Projects.Entities;

namespace Projects.Models.Projects;

public class ProjectDetailModel
{
    public Guid Id { get; set; }
    public List<PropertyDetail> Properties { get; set; } = [];
}
