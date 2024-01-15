namespace Identity.Microservice.Contracts;

public record LoginResponse(Guid Id, string Email, string Name, string Token);