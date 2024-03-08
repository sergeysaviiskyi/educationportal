using EducationPortal.Application.Interfaces;
using EducationPortal.Infrastructure.MessageBroker;

namespace EducationPortal.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("MyConnection");

            services.AddDbContext<EducationPortalContext>(
                options => options.UseSqlServer(connectionString), ServiceLifetime.Transient)
                .AddTransient<IUnitOfWork, UnitOfWork>()
                .AddScoped<IPasswordService, PasswordService>();

            services.AddDbContext<EducationPortalContext>(
                options => options.UseSqlServer(connectionString), ServiceLifetime.Transient)
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IPasswordService, PasswordService>()
                .AddScoped<IMessageProducer, MessageProducer>()
                .AddScoped<IKafkaMessageProducer, KafkaMessageProducer>();

            services.AddQuartz(options =>
            {
                var jobKey = new JobKey(nameof(DatabaseCleaningBackgroundJob));

                options
                    .AddJob<DatabaseCleaningBackgroundJob>(jobKey)
                    .AddTrigger(
                        trigger => trigger.ForJob(jobKey).WithSimpleSchedule(
                            schedule => schedule.WithIntervalInMinutes(5).RepeatForever()));

                options.UseMicrosoftDependencyInjectionJobFactory();
            });
            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }
    }
}