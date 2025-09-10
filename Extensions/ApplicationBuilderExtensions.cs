using Hangfire.MySql;

namespace Ivoluntia.BackgroudServices.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ConfigureHsts(this WebApplicationBuilder builder)
        {
            builder.Services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            builder.Services.AddHangfire(config =>
            config.UseStorage(new MySqlStorage(
            builder.Configuration.GetConnectionString("DefaultConnection"),
            new MySqlStorageOptions
            {
                TablesPrefix = "Hangfire"

            })));
        }
    }
}
