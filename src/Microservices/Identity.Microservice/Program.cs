using Identity.Microservice;
using Identity.Microservice.Endpoints;
using Serilog;
using Shared;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
    config.Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName);
    config.Enrich.WithProperty("Version", context.Configuration["Version"]);
});

builder.Services.AddPresentation();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddRepository();
builder.Services.AddHealthCheck();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHealthChecks("/health");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterIdentityEndpoints();

app.Run();
