namespace EducationPortal.PresentationWebAPI
{
    public static class DependencyInjection
    {
        public static void AddPresentation(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.AddControllers(options => { options.Filters.Add<ApiValidationFilter>(); });
            services.AddValidatorsFromAssemblyContaining(typeof(RegisterUserRequestValidator));
            services.AddScoped<ICustomValidatorFactory, CustomValidatorFactory>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.GetSection("JWTSettings")["Issuer"],
                        ValidAudience = configuration.GetSection("JWTSettings")["Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWTSettings")["Key"]))
                    };
                });

            services.AddAuthorization();
            services.AddSwaggerGen();
            services.AddEndpointsApiExplorer();
            services.AddTransient<GlobalExceptionHandlingMiddleware>();
            services.AddLogging();
        }
    }
}
