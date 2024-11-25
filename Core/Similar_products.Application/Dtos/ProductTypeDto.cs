namespace Similar_products.Application.Dtos;

public class ProductTypeDto 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public Guid ProductId { get; set; }
	public ProductDto Product { get; set; }
}

