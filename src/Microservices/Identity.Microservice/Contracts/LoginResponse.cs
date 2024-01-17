namespace Identity.Microservice.Contracts;

public record LoginResponse(string Id, string Email, string Name, string Token);