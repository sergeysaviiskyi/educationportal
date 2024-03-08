namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class CourseSkillConfiguration : IEntityTypeConfiguration<CourseSkillModel>
    {
        public void Configure(EntityTypeBuilder<CourseSkillModel> builder)
        {
            builder.HasIndex(cs => new { cs.CourseId, cs.SkillId })
                .IsUnique();
        }
    }
}
