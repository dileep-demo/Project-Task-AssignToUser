using AutoMapper;
using ProjectTaskAssign.Models;
using ProjectTaskAssign.ViewModels;

namespace ProjectTaskAssign.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map ProjectModel to ProjectViewModel and vice versa
            CreateMap<ProjectModel, ProjectViewModel>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            // Map TaskModel to TaskViewModel and vice versa
            CreateMap<TaskModel, TaskViewModel>()
                .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            // Map ProjectModel to ProjectDetailsViewModel
            CreateMap<ProjectModel, ProjectDetailsViewModel>()
                .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Tasks, opt => opt.MapFrom(src => src.Tasks));
        }
    }
}