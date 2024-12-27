using MediatR;
using Microsoft.EntityFrameworkCore;
using Projects.Common;
using Projects.Context;
using Projects.Services;

namespace Projects.Features.Projects.GetAllProjects;

public class GetProjectOptionsQuery(ProjectContext context, ICacheService cacheService)
    : IRequestHandler<GetProjectOptionRequest, List<OptionModel>>
{
    public async Task<List<OptionModel>> Handle(GetProjectOptionRequest request, CancellationToken cancellationToken)
    {
        var options = await cacheService.GetOrSetCacheAsync("projects-options", async () =>
        {
            var projects = await context.Projects.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Select(x => new OptionModel
                {
                    Key = x.Key,
                    Value = x.Name
                }).ToListAsync(cancellationToken);

            return projects;
        });
        return options ?? [];
    }
}
