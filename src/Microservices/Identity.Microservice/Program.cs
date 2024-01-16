using Identity.Microservice.Endpoints;
using Identity.Microservice.Repository;
using Shared;
using SolON.API.Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
builder.Services.AddJwt(opts => builder.Configuration.GetSection("Jwt").Bind(opts));
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.UseHealthChecks("/health");
app.RegisterUserEndpoints();

app.Run();
