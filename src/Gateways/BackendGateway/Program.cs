using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Serilog;
using Shared;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("ocelot.Development.json", optional: true, reloadOnChange: true);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
    config.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
    config.Enrich.WithProperty("Version", context.Configuration["Version"]);
});

builder.Services.AddHealthChecks();
builder.Services.AddProblemDetails();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddCors(opts =>
{
    opts.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseMiddleware<RequestLoggingMiddleware>();
app.UseExceptionHandler();
app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("CorsPolicy");

await app.UseOcelot();

app.Run();
