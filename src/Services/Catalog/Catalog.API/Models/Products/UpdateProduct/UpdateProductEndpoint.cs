namespace Catalog.API.Models.Products.UpdateProduct;

// Best practice selalu define Req dan Response di endpoint
public record UpdateProductRequest(
    Guid Id,
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price);
public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async ( UpdateProductRequest request ,ISender sender) =>
            {
                //objectnya di mapping dengan mapster agar tidak manual
                var command = request.Adapt<UpdateProductCommand>();
                //mediatr send ke handler
                var result = await sender.Send(command);

                //mapster map ke GetProductsResponse. pastikan response sama pada handler dan endpoint
                //agar bisa terhubung
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            }).WithName("Update Products")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Products")
            .WithDescription("Update Products");
    }
}