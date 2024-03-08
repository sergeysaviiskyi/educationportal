namespace EducationPortal.Application.Profiles
{
    public class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CourseModel, Course>()
                .ForMember(dest => dest.CourseMaterials, opt => opt.MapFrom(src => src.CourseMaterials.Select(m => m.Material)))
                .ForMember(dest => dest.CourseSkills, opt => opt.MapFrom(src => src.CourseSkills.Select(s => s.Skill)))
                .ReverseMap();

            CreateMap<UserCourseModel, Course>()
                .ForMember(dest=>dest.Id, opt=>opt.MapFrom(src=>src.CourseId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.LearningProgress, opt => opt.MapFrom(src => src.LearningProgress))
                .IncludeMembers(src => src.Course)
                .ReverseMap();
        }
    }
}
