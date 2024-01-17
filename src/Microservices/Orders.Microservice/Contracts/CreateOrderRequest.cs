namespace Orders.Microservice.Contracts
{
    public record CreateOrderRequest(string UserId, string BookId);
}