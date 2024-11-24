namespace Catalog.API.Models.Products.CreateProduct;

public record CreateProductRequest(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);

public record CreateProductResponse(Guid Id);

//Carter adalah Library agar memudahkan implementasi dengan minimal API
// Mapster digunakan untuk mempermudah mapping agar tidak di mapping satu per satu
public class CreateProductEndpoint : ICarterModule
{
    // ini juga bisa langsung generate dari ICarterModule
    // *** Jangan lupa Carter dan Mediatr di daftarkan di Program.cs ***
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateProductCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<CreateProductResponse>();

            return Results.Created($"/products/{response.Id}", response);
        }).WithName("Create Product")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Product")
        .WithDescription("Create Product");
    }
}