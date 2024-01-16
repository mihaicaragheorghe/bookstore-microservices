using Books.Microservice.Endpoints;
using Books.Microservice.Repository;
using Shared;
using SolON.API.Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddJwt(opts => builder.Configuration.Bind("Jwt", opts));
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

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
app.RegisterBookEndpoints();
app.RegisterAuthorEndpoints();

app.Run();
