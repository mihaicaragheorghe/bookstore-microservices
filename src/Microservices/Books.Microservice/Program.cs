using Middleware;
using Books.Microservice.Endpoints;
using Books.Microservice.Repository;
using SolON.API.Authentication.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization();
builder.Services.AddJwt(opts => builder.Configuration.GetSection("Jwt").Bind(opts));
builder.Services.AddMongoDb(builder.Configuration);
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

app.RegisterBookEndpoints();
app.RegisterAuthorEndpoints();

app.Run();
