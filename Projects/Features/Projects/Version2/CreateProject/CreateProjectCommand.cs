using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Context;
using Projects.Entities;
using Projects.Enums;
using Projects.Exceptions;
using Projects.Models.Projects;

namespace Projects.Features.Projects.Version2.CreateProject;

public class CreateProjectCommand(ProjectContext context, IMapper mapper) : IRequestHandler<CreateProjectRequest, bool>
{
    private readonly List<string> DEFAULT_PROPERTIES =
    [
        "Name",
        "Key"
    ];

    public async Task<bool> Handle(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var properties = await GetProperties(request.Properties);

        var project = new Project();

        var defaultProperties = properties.Where(x => DEFAULT_PROPERTIES.Contains(x.Property.Name)).ToList();

        MapPropertiesToProject(project, defaultProperties);

        properties = properties.Except(defaultProperties).ToList();

        context.Projects.Add(project);
        properties.ForEach(x =>
        {
            x.EntityId = project.Id;
            x.Property = null;
        });

        context.PropertyValues.AddRange(properties);

        return await context.SaveChangesAsync(cancellationToken) != 0;
    }

    private async Task<List<PropertyValue>> GetProperties(List<ProjectPropertyRequest> propertyRequests)
    {
        var propertyIds = propertyRequests.Select(x => x.PropertyId).ToList();

        var properties = await context.Properties
            .AsNoTracking()
            .Where(x => propertyIds.Any(y => y == x.Id))
            .ToListAsync();

        if (properties.Count(x => DEFAULT_PROPERTIES.Contains(x.Name)) != DEFAULT_PROPERTIES.Count)
        {
            throw new BadHttpRequestException($"Must enter project {string.Join(", ", DEFAULT_PROPERTIES)}");
        }

        var propertyValues = MapPropertyValues(propertyRequests, properties);

        return propertyValues;
    }

    private static List<PropertyValue> MapPropertyValues(List<ProjectPropertyRequest> propertyRequests,
        List<Property> properties)
    {
        var propertyMap = properties.ToDictionary(x => x.Id);

        return propertyRequests.Select(x =>
        {
            if (!propertyMap.TryGetValue(x.PropertyId, out var property))
            {
                throw new BadHttpRequestException($"Property {x.PropertyId} not found");
            }

            return new PropertyValue
            {
                Property = property,
                PropertyId = property.Id,
                Value = x.Value
            };
        }).ToList();
    }

    private static void MapPropertiesToProject(Project project, List<PropertyValue> properties)
    {
        // Get all properties of the Project class
        // var projectProperties = typeof(Project).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var name = properties.FirstOrDefault(x => x.Property.Name == nameof(Project.Name));
        var key = properties.FirstOrDefault(x => x.Property.Name == nameof(Project.Key));
        project.Name = name?.Value ?? string.Empty;
        project.Key = key?.Value ?? string.Empty;

        // foreach (var property in properties)
        // {
        //     // Find the matching property in the Project entity by name
        //     var matchingProjectProperty = projectProperties
        //         .FirstOrDefault(p => string.Equals(p.Name, property.Property.Name, StringComparison.OrdinalIgnoreCase));
        //
        //     if (matchingProjectProperty == null || !matchingProjectProperty.CanWrite) continue;
        //
        //     // Convert the value to the appropriate type and assign it
        //     var value = ConvertPropertyValue(property);
        //
        //     // Assign the value to the Project property
        //     matchingProjectProperty.SetValue(project, value);
        // }
    }

    private static object? ConvertPropertyValue(PropertyValue property)
    {
        if (string.IsNullOrWhiteSpace(property.Value))
        {
            return GetDefaultValue(property.Property.Datatype);
        }

        switch (property.Property.Datatype)
        {
            case Datatype.Text:
            case Datatype.TextArea:
                return property.Value;
            case Datatype.Number:
                // First, try parsing as an integer
                if (int.TryParse(property.Value, out var intValue))
                {
                    return intValue;
                }

                // If it doesn't parse as an integer, try parsing as a float
                if (float.TryParse(property.Value, out var floatValue))
                {
                    return floatValue;
                }

                // If it doesn't parse as a float, try parsing as a double
                if (double.TryParse(property.Value, out var doubleValue))
                {
                    return doubleValue;
                }

                return 0;
            case Datatype.Decimal:
                return decimal.TryParse(property.Value, out var decimalValue) ? decimalValue : 0;
            case Datatype.DateTime:
                return DateTime.TryParse(property.Value, out var dateValue) ? dateValue : null;
            case Datatype.TimeSpan:
                return TimeSpan.TryParse(property.Value, out var timeSpanValue) ? timeSpanValue : null;
            case Datatype.Boolean:
                return bool.TryParse(property.Value, out var boolValue) && boolValue;
            case Datatype.RadioButton:
            case Datatype.SelectList:
            case Datatype.File:
            case Datatype.MultiSelect:
            case Datatype.Person:
            default:
                return string.Empty;
        }
    }

    private static object? GetDefaultValue(Datatype datatype)
    {
        return datatype switch
        {
            Datatype.Number => 0,
            Datatype.Decimal => 0m,
            Datatype.DateTime => null,
            Datatype.TimeSpan => null,
            Datatype.Boolean => false,
            _ => string.Empty,
        };
    }
}
