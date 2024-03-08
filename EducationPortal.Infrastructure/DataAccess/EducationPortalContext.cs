namespace EducationPortal.Infrastructure.DataAccess
{
    public class EducationPortalContext : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<MaterialModel> Materials { get; set; }
        public DbSet<SkillModel> Skills { get; set; }
        public DbSet<UserCourseModel> UserCourses { get; set; }
        public DbSet<UserSkillModel> UserSkills { get; set; }
        public DbSet<CourseMaterialModel> CourseMaterials { get; set; }
        public DbSet<CourseSkillModel> CourseSkills { get; set; }
        public DbSet<RefreshTokenModel> RefreshTokens { get; set; }
        public DbSet<VerificationCodeModel> VerificationCodes { get; set; }
        public EducationPortalContext(DbContextOptions<EducationPortalContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
