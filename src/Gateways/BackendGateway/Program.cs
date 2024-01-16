using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using SolON.API.Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("ocelot.Development.json", optional: true, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();
builder.Services.AddJwt(opts => builder.Configuration.Bind("Jwt", opts));
builder.Services.AddOcelot(builder.Configuration);

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

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseHealthChecks("/health");
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();
app.UseOcelot().Wait();

app.Run();
