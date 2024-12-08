using System;
using Dashboard.Application.Features.Tasks.Common;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Dashboard.Application.Extensions;

public static class MapperProfileExtension
{
    public static IServiceCollection AddMapperProfile(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(TaskProfile));
        return services;
    }
}
