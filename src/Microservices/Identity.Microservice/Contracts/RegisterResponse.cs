namespace Identity.Microservice.Contracts;

public record RegisterResponse(Guid Id, string Email, string Name, string Token);