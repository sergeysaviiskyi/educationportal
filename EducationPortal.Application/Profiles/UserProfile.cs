namespace EducationPortal.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            //CreateMap<UserModel, User>()
            //    .ForMember(dest => dest.CoursesInProgress, opt => opt
            //    .MapFrom(src => src.Courses
            //    .Where(c => (LearningStates)c.Status == LearningStates.InProgress)
            //    .Select(c => c.Course)))
            //    .ForMember(dest => dest.FinishedCourses, opt => opt
            //    .MapFrom(src => src.Courses
            //    .Where(c => (LearningStates)c.Status == LearningStates.Learned)
            //    .Select(c => c.Course)))
            //    .ReverseMap();

            CreateMap<UserModel, User>()
                .ForMember(dest => dest.CoursesInProgress, opt => opt
                .MapFrom(src => src.Courses
                .Where(c => (LearningStates)c.Status == LearningStates.InProgress)))
                .ForMember(dest => dest.FinishedCourses, opt => opt
                .MapFrom(src => src.Courses
                .Where(c => (LearningStates)c.Status == LearningStates.Learned)))
                .ReverseMap();
        }
    }
}