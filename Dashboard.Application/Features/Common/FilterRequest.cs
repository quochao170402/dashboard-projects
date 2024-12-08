using System;
using MediatR;

namespace Dashboard.Application.Features.Common;

public class FilterRequest : IRequest<FilterResponse>
{
    public string Keyword { get; set; } = string.Empty;
    public int PageSize { get; set; } = 20;
    public int PageIndex { get; set; } = 1;
}
