namespace Catalog.API.Models.Products.GetProducts;

// Best practice selalu define Req dan Response di endpoint
public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var query = request.Adapt<GetProductsQuery>();
            //mediatr send ke handler
            var result = await sender.Send(query);

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