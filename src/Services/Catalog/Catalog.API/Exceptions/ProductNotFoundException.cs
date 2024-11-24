namespace Catalog.API.Exceptions;

public class ProductNotFoundException :Exception
{
    public ProductNotFoundException() : base("Product not found!")
    {
    }
    
    public ProductNotFoundException(string category) 
        : base($"Products with category '{category}' not found!") 
    {
    }
}