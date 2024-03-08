namespace EducationPortal.Infrastructure.EntityConfigurations
{
    public class VerificationCodeConfiguration : IEntityTypeConfiguration<VerificationCodeModel>
    {
        public void Configure(EntityTypeBuilder<VerificationCodeModel> builder)
        {
            builder.Property(p => p.Salt)
                .HasMaxLength(32)
                .IsRequired();

            builder.Property(p => p.Code)
                .IsRequired();

            builder.Property(p => p.ExpiryDate)
                .IsRequired();
        }
    }
}
