namespace Shared.Models;

public class MongoDbOptions
{
    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
}
