namespace Identity.Microservice.Contracts;

public record RegisterRequest(string Email, string Password, string Name);