namespace Catalog.API.Models.Products.GetProductById;

// Best practice selalu define Req dan Response di endpoint
// public record GetProductsRequest()

public record GetProductByIdResponse(Product Product);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                //mediatr send ke handler
                var result = await sender.Send(new GetProductByIdQuery(id));

                //mapster map ke GetProductsResponse. pastikan response sama pada handler dan endpoint
                //agar bisa terhubung
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            }).WithName("Get Product by Id")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product by Id")
            .WithDescription("Get Product by Id");
    }
}