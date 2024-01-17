using Orders.Microservice.Contracts;
using Orders.Microservice.Models;
using Orders.Microservice.Repository;

namespace Orders.Microservice.Endpoints;

public static class OrdersModule
{
    public static void RegisterOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/api/orders/user/{userId}", async (string userId, IOrderRepository repository) =>
        {
            var orders = await repository.GetByUserIdAsync(userId);
            return Results.Ok(orders);
        });

        app.MapGet("/api/orders/{id}", async (string id, IOrderRepository repository) =>
        {
            var order = await repository.GetByIdAsync(id);
            return order is null ? Results.NotFound() : Results.Ok(order);
        });

        app.MapPost("/api/orders", async (CreateOrderRequest request, IOrderRepository repository) =>
        {
            var order = new Order(request.UserId, request.BookId);
            var createdOrder = await repository.CreateAsync(order);
            return Results.Created($"/orders/{createdOrder.Id}", createdOrder);
        });

        app.MapDelete("/api/orders/{id}", async (string id, IOrderRepository repository) =>
        {
            var deleted = await repository.DeleteAsync(id);
            return deleted ? Results.Ok() : Results.NotFound();
        });
    }
}