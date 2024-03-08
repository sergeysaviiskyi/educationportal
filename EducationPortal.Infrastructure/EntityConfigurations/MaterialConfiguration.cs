namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class MaterialConfiguration : IEntityTypeConfiguration<MaterialModel>
    {
        public void Configure(EntityTypeBuilder<MaterialModel> builder)
        {
            builder.HasDiscriminator<string>("MaterialType")
                .HasValue<VideoModel>("Video")
                .HasValue<EBookModel>("EBook")
                .HasValue<ArticleModel>("Article");

            builder.HasMany(m => m.Courses)
                .WithOne(cm => cm.Material)
                .HasForeignKey(cm => cm.MaterialId);
        }
    }
}
