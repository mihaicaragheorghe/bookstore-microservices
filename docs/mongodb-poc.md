# Proof of Concept: MongoDB
---

MongoDb is a NoSQL database, it uses JSON-like documents with optional schemas. It is a document-oriented database.

# NoSQL vs SQL

|                       | SQL Databases | NoSQL Databases |
|-----------------------|---------------|-----------------|
| Data Storage Model    | Tables with fixed rows and columns | Document: JSON documents, Key-value: key-value pairs, Wide-column: tables with rows and dynamic columns, Graph: nodes and edges |
| Development History   | Developed in the 1970s with a focus on reducing data duplication | Developed in the late 2000s with a focus on scaling and allowing for rapid application change driven by agile and DevOps practices. |
| Examples              | Oracle, MySQL, Microsoft SQL Server, and PostgreSQL | Document: MongoDB and CouchDB, Key-value: Redis and DynamoDB, Wide-column: Cassandra and HBase, Graph: Neo4j and Amazon Neptune |
| Primary Purpose       | General purpose | Document: general purpose, Key-value: large amounts of data with simple lookup queries, Wide-column: large amounts of data with predictable query patterns, Graph: analyzing and traversing relationships between connected data |
| Schemas               | Rigid | Flexible |
| Scaling               | Vertical (scale-up with a larger server) | Horizontal (scale-out across commodity servers) |
| Multi-Record ACID Transactions | Supported | Most do not support multi-record ACID transactions. However, some — like MongoDB — do. |
| Joins                 | Typically required | Typically not required |
| Data to Object Mapping | Requires ORM (object-relational mapping) | Many do not require ORMs. MongoDB documents map directly to data structures in most popular programming languages. |

Source: https://www.mongodb.com/nosql-explained/nosql-vs-sql

# Ideal cases for MongoDb
---

- **Big Data Applications**: MongoDB's ability to handle large volumes of diverse data makes it suitable for big data applications.

- **Real-Time Applications**: For APIs that require real-time data processing and streaming (like chat applications or live updates), MongoDB's replication and change streams features enable these capabilities efficiently.

- **Content Management Systems**: Due to its schema flexibility, it’s great for CMS where data structure can change over time.

- **High Write Load**: MongoDB handles high levels of write activity very efficiently, making it a good choice for logging and real-time analytics.

- **Mobile Apps**: MongoDB’s scalability and flexibility are beneficial for mobile applications that need to evolve rapidly.

- **Geospatial Applications**: For applications requiring location-based services, MongoDB's geospatial features are highly beneficial.

It's important to note that while MongoDB excels in these areas, SQL databases are still preferred in scenarios requiring complex joins, transactional integrity, and normalized data structures. The choice between MongoDB and an SQL database ultimately depends on the specific requirements of the application or project.

# Pros
---

- **Schema-less Nature**: MongoDB is a document database that allows you to store data in JSON-like documents. This flexibility can be particularly useful for applications that require a high degree of data variety and rapid iteration.

- **ORM-like syntax**: MongoDB's C# driver provides a high-level abstraction that can make database interactions more intuitive for developers familiar with object-oriented programming.

- **Scalability**: MongoDB offers horizontal scalability, which is beneficial for applications with large and growing data sets or high user loads.

- **Performance**: Its document model can lead to faster queries as related data is often stored together. This can be especially beneficial for read-heavy applications.

- **Rapid Development**: MongoDB's flexible schema allows for quick iteration and evolution of your data model. This is especially beneficial in agile development environments where requirements can change frequently. In a .NET Web API context, this means you can adapt your data model without needing to perform complex migrations or schema updates.

- **Object-Oriented Nature**: MongoDB stores data in a format (BSON, similar to JSON) that is more naturally aligned with object-oriented programming languages like C#. This can simplify the code in your .NET API, as you can serialize and deserialize data objects directly to and from the database without needing complex ORM (Object-Relational Mapping) layers.

- **Strong Community and Ecosystem**: MongoDB has a large community and a robust ecosystem of tools and extensions, making it easier to find support and resources.

- **Integration with .NET Ecosystem**: MongoDB has good support and integration with the .NET ecosystem. Official MongoDB drivers for .NET and extensive documentation make it easier for developers to integrate MongoDB into their .NET applications.

# Cons
---

- **Data Consistency**: MongoDB, being a NoSQL database, follows the BASE (Basically Available, Soft state, Eventual consistency) model rather than the ACID (Atomicity, Consistency, Isolation, Durability) model typical of SQL databases. This can be a concern in applications where data consistency is critical.

- **Transaction Support**: Unlike traditional SQL databases, MongoDB's support for multi-document transactions is relatively recent and can be more complex to implement. If your application requires complex transactions (e.g., involving multiple operations that must be completed together or not at all), you might find SQL databases more straightforward.

- **Schema Design Considerations**: While schema flexibility is an advantage, it can also be a downside if not managed properly. Poorly designed schemas can lead to performance issues, data redundancy, and difficulty in maintaining data integrity.

- **Data Integrity:** The lack of a fixed schema can lead to data integrity challenges, particularly in large and complex applications.

- **Learning Curve:** Developers accustomed to SQL databases may face a learning curve understanding MongoDB's data model and query language.

- **Join Operations:** While MongoDB has improved its join capabilities, performing complex joins is still difficult compared to relational databases.

- **Memory Usage:** MongoDB's in-memory storage engine can lead to higher memory usage, which might be a concern for large datasets or constrained environments.

- **Backup and Restore Complexity:** Due to its distributed nature, backing up and restoring MongoDB can be more complex compared to traditional SQL databases.

- **Reporting and Analytics**: MongoDB is not inherently optimized for complex reporting and analytics queries that are typically easier to perform with SQL databases. This can be a limitation for applications where reporting is a core feature.

# Scalability
---

- **Application and Database Decoupling**: In a scalable architecture, it's often advisable to decouple the application logic from the database. This allows each component to scale independently. With MongoDB’s flexible schema and API-driven interaction, it's easier to manage this decoupling in a .NET environment.

- **Microservices Architecture Compatibility**: MongoDB works well with microservices architectures, which are commonly used for scalable applications. In a microservices-based .NET API, different services can interact with MongoDB independently, allowing each service to scale as needed.

- **Distributed System Support**: MongoDB’s sharding and replication features support distributed systems well. This means that as your application grows and potentially spans multiple servers or even geographical regions, MongoDB can handle this distribution effectively.


**TL;DR**: MongoDB excels in a .NET Web API context due to its schema flexibility, horizontal scalability, and efficient handling of large and varied data sets, making it ideal for rapid development and high-volume, high-traffic applications. It supports distributed systems with features like sharding and replica sets, facilitating scalability and high availability. However, it might have challenges in complex transactions and data consistency compared to SQL databases. SQL databases, with their rigid schemas and strong transactional integrity, are better for scenarios requiring complex relational data operations but may face scalability challenges that MongoDB can more easily handle. Overall, MongoDB is well-suited for dynamic, large-scale applications, while SQL is preferred for applications needing strict relational data management.

# .NET integration
---

Integrating MongoDB with a .NET Web API is streamlined, thanks to the MongoDB C# driver, which provides a .NET interface for interacting with MongoDB. This driver allows you to perform CRUD (Create, Read, Update, Delete) operations, manage indexes, and execute other database operations in a way that is idiomatic to C#.

### Setting up the MongoDb driver
First, install the MongoDb C# driver
``` bash
dotnet add package MongoDB.Driver
```

### Dependency Injection

``` csharp
public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
{
    services.Configure<MongoDbOptions>(configuration.GetSection(MongoDbOptions.SectionName));
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
```
This will inject a `IMongoClient`, which we can later use to perform database operations.

### Connecting to MongoDb

To connect to a MongoDB instance, you create a MongoClient object with the connection string:

``` csharp 
var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("Books");
var collection = database.GetCollection<User>(User.CollectionName);
```

Using dependency injection

``` csharp
public class UserRepository(IMongoDatabase db) : IUserRepository
{
    private readonly IMongoCollection<User> _users = db.GetCollection<User>(User.CollectionName);
}
```

### Data model

You can define a C# class that represents the MongoDB document. MongoDB stores data in BSON format, but the driver handles the serialization from your C# class to BSON:

``` csharp
public class User
{
    public static readonly string CollectionName = nameof(User);

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; } = null!;

    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string Name { get; init; } = null!;
}
```

### CRUD Operations

**Create**
``` csharp
public async Task<User> AddUserAsync(User user)
{
    await _users.InsertOneAsync(user);
    return user;
}
```

**Read**
``` csharp
public async Task<User?> GetUserByIdAsync(string id) =>
    await _users.Find(u => u.Id == id).SingleOrDefaultAsync();
```

**Update**
``` csharp
public async Task<bool> UpdateUserAsync(User user)
{
    var result = await _users.ReplaceOneAsync(u => u.Id == user.Id, user);
    return result.IsAcknowledged && result.ModifiedCount > 0;
}
```

**Delete**
``` csharp
public async Task<bool> DeleteUserAsync(string id)
{
    var result = await _users.DeleteOneAsync(u => u.Id == id);
    return result.IsAcknowledged && result.DeletedCount > 0;
}
```

**Advanced Queries**
You can also perform more complex queries and operations, like aggregations:
``` csharp
public async Task<AuthorBooksAggregate?> GetAuthorWithBooksAsync(string id)
{
    var aggregate = _authors.Aggregate()
        .Match(author => author.Id == id)
        .Lookup<Author, Book, AuthorBooksAggregate>(
            foreignCollection: _books,
            localField: author => author.Id,
            foreignField: book => book.AuthorId,
            @as: aggregate => aggregate.Books);

    return await aggregate.SingleOrDefaultAsync();
}
```

# Containerization
---

MongoDB can be run in Docker containers, which offers a lightweight, portable, and consistent environment for development, testing, and production. Containers encapsulate MongoDB and its dependencies, ensuring that it runs the same way regardless of the environment.

Tools like Kubernetes can be used to manage MongoDB containers, especially in a microservices architecture. They handle tasks like scaling, load balancing, and automated rolling updates.

### Best practices

- **Immutable Containers**: Aim for immutable container deployments, where containers are replaced rather than updated, ensuring consistency and reliability in deployments.

- **Persistent storage**: To manage data persistence in containers, you should use volumes. Docker volumes are directories (or files) that exist outside of the default union file system and are designed to persist data.

- **Backup**: Regular backups are crucial for data persistence. This can be achieved through automated scripts or MongoDB’s own backup solutions that can be integrated into your containerized environment.

- **Environment Parity**: Keep development, testing, and production environments as similar as possible. Containers are excellent for this purpose as they encapsulate the application and its environment.

- **Monitoring and Logging**: Implement robust monitoring and logging for your MongoDB containers to quickly detect and respond to issues.