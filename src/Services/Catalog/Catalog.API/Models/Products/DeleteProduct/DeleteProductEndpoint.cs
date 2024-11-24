namespace Catalog.API.Models.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccessfull);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                //mediatr send ke handler
                var result = await sender.Send(new DeleteProductCommand(id));

                //mapster map ke GetProductsResponse. pastikan response sama pada handler dan endpoint
                //agar bisa terhubung
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            }).WithName("Delete Product by Id")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product by Id")
            .WithDescription("Delete Product by Id");
    }
}