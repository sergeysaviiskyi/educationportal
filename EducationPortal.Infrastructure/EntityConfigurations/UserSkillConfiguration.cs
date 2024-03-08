namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class UserSkillConfiguration : IEntityTypeConfiguration<UserSkillModel>
    {
        public void Configure(EntityTypeBuilder<UserSkillModel> builder)
        {
            builder.HasIndex(us => new { us.UserId, us.SkillId })
                .IsUnique();

            //builder.HasOne(u => u.User)
            //    .WithMany(us => us.LearnedSkills)
            //    .HasForeignKey(us => us.SkillId);
        }
    }
}
