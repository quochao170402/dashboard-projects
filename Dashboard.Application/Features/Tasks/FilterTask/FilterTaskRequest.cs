using System;
using System.ComponentModel.DataAnnotations;
using Dashboard.Application.Features.Common;

namespace Dashboard.Application.Features.Tasks.FilterTask;

public class FilterTaskRequest : FilterRequest
{
    [Required]
    public Guid ProjectId { get; set; }
}
