namespace Books.Microservice.Contracts
{
    public record CreateBookRequest(string AuthorId, string Title, string Description);
}