using Dashboard.API.Entities;
using Dashboard.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        return StatusCode(200, await _projectRepository.Get());
    }

    [HttpGet]
    public async Task<IActionResult> AddProject()
    {
        return StatusCode(200, await _projectRepository.Add(new Project
        {
            Name = "Name",
            Description = "Description",
            Key = "Key",
            LogoUrl = "LogoUrl",
            LeaderId = "LeaderId",
            Url = "Url"
        }));
    }
}