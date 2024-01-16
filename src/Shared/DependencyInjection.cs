using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Shared.Models;

namespace Shared;

public static class DependencyInjection
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection("MongoDb"));
        services.AddSingleton<IMongoClient, MongoClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoDbOptions>>();

            return new MongoClient(options.Value.ConnectionString);
        });
        services.AddScoped(sp =>
        {
            var options = sp.GetRequiredService<IOptions<MongoDbOptions>>();
            var client = sp.GetRequiredService<IMongoClient>();

            return client.GetDatabase(options.Value.Database);
        });

        return services;
    }
}