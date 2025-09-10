using Ivoluntia.BackgroudServices.Data.Repository.Implementation;
using Ivoluntia.BackgroudServices.Data.Repository.Interface;

namespace Ivoluntia.BackgroudServices.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IEmailJobService, EmailJobService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<GmailService>();
            services.AddScoped<IEmailServiceFactory, EmailServiceFactory>();
            services.AddScoped<IUnitofWork, UnitofWork>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "iVoluntia Background Services",
                    Version = "v1"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                Enter 'Bearer' [space] and then your token in the text input below.
                                Example: 'Bearer ey12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
        public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration config)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin()
                                .AllowAnyMethod()
                                .AllowAnyHeader();
                });
                options.AddPolicy("Filter", policyBuilder =>
                {
                    policyBuilder.WithOrigins(config.GetSection("CORS:AllowedOrigins").Value!.Split(','))
                                .WithMethods(config.GetSection("CORS:AllowedMethods").Value!.Split(','))
                                .WithHeaders(config.GetSection("CORS:AllowedHeaders").Value!.Split(','))
                                .AllowCredentials();
                });
            });

            return services;
        }
        public static IServiceCollection AddCustomDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<iVoluntiaDataContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("DefaultConnection")!,
                    ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection")!)
                )
            );

            return services;
        }
        public static IServiceCollection AddCustomIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>()
                    .AddEntityFrameworkStores<iVoluntiaDataContext>()
                    .AddDefaultTokenProviders();

            return services;
        }
    }
}
