using AutoMapper;
using Domain.Commands;
using Domain.DataModels;
using Domain.ViewModel;

namespace WebApi.AutoMapper
{
    public class TaskProfile : Profile
    {
        public TaskProfile()
        {
            CreateMap<CreateTaskCommand, Tasks>();
            CreateMap<UpdateTaskCommand, Tasks>();
            CreateMap<Tasks, TaskVm>();
        }
    }
}