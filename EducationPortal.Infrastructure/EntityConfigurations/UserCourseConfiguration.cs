namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourseModel>
    {
        public void Configure(EntityTypeBuilder<UserCourseModel> builder)
        {
            builder.HasIndex(uc => new { uc.UserId, uc.CourseId })
                .IsUnique();
        }
    }
}
