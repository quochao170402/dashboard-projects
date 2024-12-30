using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Constants;
using Projects.Context;
using Projects.Entities;
using Projects.Enums;
using Projects.Models;
using Projects.Models.Projects;

namespace Projects.Features.Projects.GetProjectsPaging;

public class GetProjectPagingQuery(ProjectContext context, IServiceProvider serviceProvider)
    : IRequestHandler<GetProjectsPaging, (List<ProjectDetailModel> projects, int count)>
{

    public async Task<(List<ProjectDetailModel> projects, int count)> Handle(GetProjectsPaging request, CancellationToken cancellationToken)
    {
        var (projects, count) = await GetProjectsAsync(request, cancellationToken);

        var getPropertiesTask = GetPropertiesAsync(cancellationToken);
        var getPropertyValuesTask = GetPropertyValuesByProjectIds(projects.Select(x => x.Id), cancellationToken);
        await Task.WhenAll(getPropertiesTask, getPropertyValuesTask);

        var properties = getPropertiesTask.Result;
        var propertyValues = getPropertyValuesTask.Result;

        var valueMap = propertyValues.GroupBy(x => x.EntityId)
            .ToDictionary(
                x => x.Key,
                x => x.ToDictionary(y=>y.PropertyId));

        var result = new List<ProjectDetailModel>();

        foreach (var project in projects)
        {
            if (!valueMap.TryGetValue(project.Id, out var propertyValuesOfProject))
            {
                propertyValuesOfProject = new Dictionary<Guid, PropertyValue>();
            }

            var values = properties.Where(x=> x.IsDefault).Select(x =>
            {
                var propertyValue = string.Empty;
                if (!propertyValuesOfProject.TryGetValue(x.Id, out var value))
                {
                    // Get the type of the object (Project in this case)
                    var type = project.GetType();

                    // Get the property info for the specified property
                    var propertyInfo = type.GetProperty(x.Name);

                    if (propertyInfo != null && propertyInfo.GetValue(project) != null)
                    {
                        propertyValue = propertyInfo.GetValue(project)?.ToString()!;
                    }
                }
                else
                {
                    propertyValue = value.Value;
                }

                return new PropertyDetail
                {
                    Id = x.Id,
                    Name = x.Name,
                    Label = x.Label,
                    Datatype = x.Datatype,
                    IsDefault = x.IsDefault,
                    Value = propertyValue
                };
            }).ToList();

            var detail = new ProjectDetailModel
            {
                Id = project.Id,
                Properties = values
            };

            result.Add(detail);
        }

        return (result, count);
    }

    private async Task<(List<Project> projects, int count)> GetProjectsAsync(GetProjectsPaging request, CancellationToken cancellationToken)
    {
        // Create a new DbContext instance
        using var scope = serviceProvider.CreateScope();
        await using var scopedContext = scope.ServiceProvider.GetRequiredService<ProjectContext>();

        // Query total count of projects
        var count = await scopedContext.Projects.CountAsync(cancellationToken);

        // Fetch paginated projects
        var projects = await scopedContext.Projects.OrderBy(x => x.CreatedAt)
            .AsNoTracking()
            .Skip(request.PageSize * (request.PageIndex - 1))
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return (projects, count);
    }

    private async Task<List<Property>> GetPropertiesAsync(CancellationToken cancellationToken)
    {
        // Create a new DbContext instance
        using var scope = serviceProvider.CreateScope();
        await using var scopedContext = scope.ServiceProvider.GetRequiredService<ProjectContext>();

        var defaultProperties = typeof(DefaultProjectProperties)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(x=> x.GetValue(null) != null)
            .Select(x=>(Property)x.GetValue(null)!)
            .Select(x=> new Property(x.Name, x.Datatype))
            .ToList();

        // Fetch properties
        var properties =  await scopedContext.Properties
            .AsNoTracking()
            .Where(x => !x.IsDeleted && x.PropertyType == PropertyType.Project)
            .ToListAsync(cancellationToken);

        return defaultProperties.Concat(properties).ToList();
    }

    private async Task<List<PropertyValue>> GetPropertyValuesByProjectIds(IEnumerable<Guid> projectIds, CancellationToken cancellationToken)
    {
        // Create a new DbContext instance
        using var scope = serviceProvider.CreateScope();
        await using var scopedContext = scope.ServiceProvider.GetRequiredService<ProjectContext>();

        return await scopedContext.PropertyValues
            .AsNoTracking()
            .Where(x => !x.IsDeleted && projectIds.Contains(x.EntityId))
            .ToListAsync(cancellationToken);
    }
}
