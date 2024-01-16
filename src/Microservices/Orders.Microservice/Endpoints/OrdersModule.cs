using Orders.Microservice.Models;
using Orders.Microservice.Repository;

namespace Orders.Microservice.Endpoints;

public static class OrdersModule
{
    public static void RegisterOrderEndpoints(this WebApplication app)
    {
        app.MapGet("/orders/user/{userId}", async (Guid userId, IOrderRepository repository) =>
        {
            var orders = await repository.GetByUserIdAsync(userId);
            return Results.Ok(orders);
        });

        app.MapGet("/orders/{id}", async (Guid id, IOrderRepository repository) =>
        {
            var order = await repository.GetByIdAsync(id);
            return order is null ? Results.NotFound() : Results.Ok(order);
        });

        app.MapPost("/orders", async (Order order, IOrderRepository repository) =>
        {
            var createdOrder = await repository.CreateAsync(order);
            return Results.Created($"/orders/{createdOrder.Id}", createdOrder);
        });

        app.MapPut("/orders/{id}", async (Guid id, Order order, IOrderRepository repository) =>
        {
            var updated = await repository.UpdateAsync(order);
            return updated ? Results.Ok() : Results.NotFound();
        });

        app.MapDelete("/orders/{id}", async (Guid id, IOrderRepository repository) =>
        {
            var deleted = await repository.DeleteAsync(id);
            return deleted ? Results.Ok() : Results.NotFound();
        });
    }
}