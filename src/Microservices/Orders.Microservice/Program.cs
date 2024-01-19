using Orders.Microservice;
using Orders.Microservice.Endpoints;
using Orders.Microservice.Repository;
using Serilog;
using Shared;
using Shared.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, config) =>
    config.ReadFrom.Configuration(context.Configuration));

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
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.RegisterOrderEndpoints();

app.Run();
