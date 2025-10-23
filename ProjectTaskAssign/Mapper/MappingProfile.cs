using AutoMapper;
using ProjectTaskAssign.Models;
using ProjectTaskAssign.ViewModels;

namespace ProjectTaskAssign.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProjectModel, ProjectViewModel>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            //CreateMap<TaskModel, TaskViewModel>()
            //    .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
            //    .ReverseMap();
            CreateMap<TaskViewModel, TaskModel>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.TaskId))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
            .ForMember(dest => dest.AssigneeId, opt => opt.MapFrom(src => src.AssigneeId))
            .ReverseMap();

            CreateMap<ProjectModel, ProjectDetailsViewModel>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));
            
            CreateMap<AssigneeModel, AssigneeViewModel>()
              .ForMember(dest => dest.AssigneeId, opt => opt.MapFrom(src => src.Id))
              .ReverseMap();
        }
    }
}
