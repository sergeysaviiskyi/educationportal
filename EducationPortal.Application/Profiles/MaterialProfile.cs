namespace EducationPortal.Application.Profiles
{
    public class MaterialProfile : Profile
    {
        public MaterialProfile()
        {
            CreateMap<MaterialModel, Material>()
                .Include<VideoModel, Video>()
                .Include<EBookModel, EBook>()
                .Include<ArticleModel, Article>()
                .ReverseMap();

            CreateMap<VideoModel, Video>()
                .ReverseMap();
            CreateMap<EBookModel, EBook>()
                .ReverseMap();
            CreateMap<ArticleModel, Article>()
                .ReverseMap();
        }
    }
}
