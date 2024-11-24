
namespace Catalog.API.Models.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Category,
    string Description,
    string ImageFile,
    decimal Price
    ) :ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

// ICommandHandler diamabil dari blocking blocks yang telah kita buat dengan mediatR yang membutuhkan command dan response
// Response tidak boleh null seperti yang telah kita buat
internal class CreateProductCommandHandler(IDocumentSession session) :ICommandHandler<CreateProductCommand,CreateProductResult>
{
    // Handle cara gampangnya generate dari ICommandHandler lalu di logic
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create Product Entity from command object
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        // save to db
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        //return CreateProductResult result
        return new CreateProductResult(product.Id);
    }
}