namespace Shared.Models;

public class MongoDbOptions
{
    public const string SectionName = "MongoDb";

    public string ConnectionString { get; set; } = null!;
    public string Database { get; set; } = null!;
}
