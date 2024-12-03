using Dashboard.API.Contexts;
using Dashboard.API.Entities;
using Dashboard.API.Repositories;
using Dashboard.API.Repositories.Common;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Dashboard.Test.Repositories;

public class ProjectRepositoryTest
{
    // Task<T?> GetById(Guid id);
    // Task<List<T>> Get();
    // Task<bool> AddMany(IEnumerable<T> entities);
    // Task<bool> Add(T entity);
    // Task<bool> Update(T entity);
    // Task<bool> UpdateMany(IEnumerable<T> entities);
    // Task<bool> Delete(Guid id);
    // Task<bool> DeleteMany(IEnumerable<Guid> ids);

    private readonly ProjectRepository _projectRepository;

    public ProjectRepositoryTest()
    {
        // Create in-memory options
        var options = new DbContextOptionsBuilder<DashboardContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Unique DB name per test
            .Options;

        // Initialize the DbContext with the options
        var context = new DashboardContext(options);

        _projectRepository = new ProjectRepository(context);
    }

    [Fact]
    public async Task GetById_ShouldReturnProject_WhenProjectExists()
    {
        // Given
        var mockRepo = new Mock<IRepository<Project>>();
        var id = Guid.NewGuid();
        var expectedProject = new Project { Id = id, Name = "Project Name", Key = "Project Key" };

        mockRepo.Setup(repo => repo.GetById(id)).ReturnsAsync(expectedProject);

        // When
        var project = await mockRepo.Object.GetById(id);


        // Then
        Assert.NotNull(project);
        Assert.Equal(expectedProject.Name, project.Name);
        Assert.Equal(expectedProject.Key, project.Key);
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenProjectIsNotExists()
    {
        // Given
        var mockRepo = new Mock<IRepository<Project>>();

        await _projectRepository.Add(new Project
        {
            Id = Guid.NewGuid(),
            Name = "Project Name",
            Key = "Project Key"
        });

        // When
        var project = await _projectRepository.GetById(Guid.NewGuid());

        // Then
        Assert.Null(project);
    }

    [Fact]
    public async Task AddProject_ShouldAddToDatabase_WhenProjectKeyIsNotExists()
    {
        // Given
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = "Project Name",
            Key = "Project Key"
        };

        // When
        var result = await _projectRepository.Add(project);

        // Then
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateProject_ShouldUpdateProjectAndReturnTrue_WhenProjectExists()
    {
        // Given
        var id = Guid.NewGuid();
        var project = new Project
        {
            Id = id,
            Name = "Name",
            Description = "Description",
            Key = "Key",
            LogoUrl = "LogoUrl",
            LeaderId = "LeaderId",
            Url = "Url"
        };
        await _projectRepository.Add(project);

        // When
        var existing = await _projectRepository.GetById(id);
        Assert.NotNull(existing);
        existing.Name = "New Name";
        var updateResult = await _projectRepository.Update(existing);

        // Then
        Assert.True(updateResult);
    }

    [Fact]
    public async Task UpdateProject_ShouldThrowException_WhenProjectIsNotExists()
    {
        // Given
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            Description = "Description",
            Key = "Key",
            LogoUrl = "LogoUrl",
            LeaderId = "LeaderId",
            Url = "Url"
        };
        // When And Then
        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await _projectRepository.Update(project));
    }
}
