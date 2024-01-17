namespace Identity.Microservice.Contracts;

public record RegisterResponse(string id, string Email, string Name, string Token);