namespace EducationPortal.Application.Profiles
{
    public class SkillProfile : Profile
    {
        public SkillProfile()
        {
            CreateMap<SkillModel, Skill>()
                .ReverseMap();

            CreateMap<UserSkillModel, Skill>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SkillId))
                .IncludeMembers(src => src.Skill)
                .ReverseMap();
        }
    }
}
    