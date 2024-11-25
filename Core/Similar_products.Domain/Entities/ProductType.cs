namespace Similar_products.Domain.Entities;

public class ProductType 
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public Guid ProductId { get; set; }
	public Product Product { get; set; }
}
