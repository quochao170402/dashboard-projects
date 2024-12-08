using System;
using AutoMapper;
using Dashboard.Application.Features.Tasks.CreateTask;
using Dashboard.Application.Features.Tasks.UpdateTask;
using Dashboard.Domain.TaskDomain;

namespace Dashboard.Application.Features.Tasks.Common;

public class TaskProfile : Profile
{
    public TaskProfile()
    {
        CreateMap<TaskEntity, TaskDetailResponse>();
        CreateMap<TaskEntity, TaskResponse>();
        CreateMap<CreateTaskRequest, UpdateTaskRequest>();
        CreateMap<CreateTaskRequest, TaskEntity>()
            .ForAllMembers(options => options.Condition((src, dest, srcMember) =>
                srcMember != null));
        CreateMap<UpdateTaskRequest, TaskEntity>()
            .ForAllMembers(options => options.Condition((src, dest, srcMember) =>
                srcMember != null));
    }
}
