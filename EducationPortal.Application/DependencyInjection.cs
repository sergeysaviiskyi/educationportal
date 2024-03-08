namespace EducationPortal.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddAutoMapper(Assembly.Load("EducationPortal.Application"));

            var servicesTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service"))
                .ToList();

            foreach (var type in servicesTypes)
            {
                var parentType = type.GetInterfaces().FirstOrDefault();
                services.AddScoped(parentType, type);
            }

            services.Configure<EmailConfiguration>(configuration.GetSection("EmailConfiguration"));
            services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));
            services.Configure<BrokerSettings>(configuration.GetSection("BrokerSettings"));
            services.Configure<KafkaSettings>(configuration.GetSection("KafkaSettings"));
        }
    }
}
