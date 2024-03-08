namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasMany(u => u.Courses)
                .WithOne(uc => uc.User)
                .HasForeignKey(uc => uc.UserId);

            builder.HasMany(u => u.LearnedSkills)
                .WithOne(us => us.User)
                .HasForeignKey(us => us.UserId);

            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasOne(u => u.RefreshToken)
                .WithOne(t => t.User)
                .HasForeignKey<UserModel>(u => u.RefreshTokenId);

            builder.HasOne(u => u.VerificationCode)
                .WithOne(v => v.User)
                .HasForeignKey<UserModel>(u => u.VerificationCodeId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
//add-migration JoinedUserWithVerificationCodeTables