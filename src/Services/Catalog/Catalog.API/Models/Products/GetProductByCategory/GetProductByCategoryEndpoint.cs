namespace Catalog.API.Models.Products.GetProductByCategory;

// Best practice selalu define Req dan Response di endpoint
// public record GetProductsRequest()

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductByCategoryEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                //mediatr send ke handler
                var result = await sender.Send(new GetProductByCategoryQuery(category));

                //mapster map ke GetProductsResponse. pastikan response sama pada handler dan endpoint
                //agar bisa terhubung
                var response = result.Adapt<GetProductByCategoryResponse>();
                return Results.Ok(response);
            }).WithName("Get Product by category")
            .Produces<GetProductByCategoryResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by category")
            .WithDescription("Get Product by category");
    }
}