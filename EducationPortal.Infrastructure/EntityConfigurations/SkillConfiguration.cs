namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<SkillModel>
    {
        public void Configure(EntityTypeBuilder<SkillModel> builder)
        {
            builder.HasMany(u => u.LearnedByUsers)
                .WithOne(us => us.Skill)
                .HasForeignKey(us => us.SkillId);

            builder.HasMany(u => u.Courses)
                .WithOne(cs => cs.Skill)
                .HasForeignKey(cs => cs.SkillId);
        }
    }
}
