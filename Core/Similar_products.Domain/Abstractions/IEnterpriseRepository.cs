using Similar_products.Domain.Entities;

namespace Similar_products.Domain.Abstractions;

public interface IEnterpriseRepository 
{
	Task<IEnumerable<Enterprise>> Get(bool trackChanges);
	Task<Enterprise?> GetById(Guid id, bool trackChanges);
    Task Create(Enterprise entity);
    void Delete(Enterprise entity);
    void Update(Enterprise entity);
    Task SaveChanges();
}

