using AutoMapper;
using Projects.Controllers.Payload;
using Projects.Entities;
using Projects.Models;
using Projects.Models.Projects;

namespace Projects.Profiles;

public class ProjectProfile : Profile
{
    public ProjectProfile()
    {
        CreateMap<Project, ProjectModel>();
        CreateMap<AddProjectRequest, Project>();
        CreateMap<UpdateProjectRequest, Project>();
    }
}
