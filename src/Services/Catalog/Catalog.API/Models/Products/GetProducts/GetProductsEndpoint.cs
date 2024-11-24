namespace Catalog.API.Models.Products.GetProducts;

// Best practice selalu define Req dan Response di endpoint
// public record GetProductsRequest()
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
        {
            //mediatr send ke handler
            var result = await sender.Send(new GetProductsQuery());

            //mapster map ke GetProductsResponse. pastikan response sama pada handler dan endpoint
            //agar bisa terhubung
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        }).WithName("Get Products")
            .Produces<GetProductsResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");
    }
}