using Ivoluntia.BackgroudServices.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHangfireServer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddCustomSwagger();
builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomCors(builder.Configuration);
builder.Services.AddCustomDatabase(builder.Configuration);
builder.Services.AddCustomIdentity();
builder.Services.AddScoped<NetworkFilter>();
builder.ConfigureHsts();

var app = builder.Build();

app.UseHangfireDashboard("/hangfire");


RecurringJob.AddOrUpdate<IEmailJobService>(
    "send-unsent-emails",
    service => service.SendNotificationEmailAsync(),
    "*/10 * * * *"
);

    app.UseSwagger();
    app.UseSwaggerUI();

app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

if (Convert.ToBoolean(builder.Configuration.GetSection("CORS:Enabled").Value)) app.UseCors("Filter");
else app.UseCors("AllowAll");

app.UseHsts();
app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
