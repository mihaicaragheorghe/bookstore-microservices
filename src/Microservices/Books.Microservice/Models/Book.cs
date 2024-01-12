namespace Books.Microservice.Models;

public class Book
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
}