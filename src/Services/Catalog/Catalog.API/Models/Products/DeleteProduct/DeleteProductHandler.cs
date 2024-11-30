namespace Catalog.API.Models.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccessfull);

public class DeleteProductHandler : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductHandler()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product ID is required");  
    }
}

internal class DeleteProductCommandHandler 
(IDocumentSession session,ILogger<DeleteProductCommandHandler> logger)
:ICommandHandler<DeleteProductCommand,DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
            logger.LogInformation("DeleteProductHandler.Handle called with {@Command}",command);
            session.Delete<Product>(command.Id);
            await session.SaveChangesAsync(cancellationToken);
            return new DeleteProductResult(true);
    }
}