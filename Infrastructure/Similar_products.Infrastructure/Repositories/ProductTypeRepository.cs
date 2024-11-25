using Microsoft.EntityFrameworkCore;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;

namespace Similar_products.Infrastructure.Repositories;

public class ProductTypeRepository(AppDbContext dbContext) : IProductTypeRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(ProductType entity) => await _dbContext.ProductTypes.AddAsync(entity);

    public async Task<IEnumerable<ProductType>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.ProductTypes.Include(e => e.Product).AsNoTracking() 
            : _dbContext.ProductTypes.Include(e => e.Product)).ToListAsync();

    public async Task<ProductType?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.ProductTypes.Include(e => e.Product).AsNoTracking() :
            _dbContext.ProductTypes.Include(e => e.Product)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(ProductType entity) => _dbContext.ProductTypes.Remove(entity);

    public void Update(ProductType entity) => _dbContext.ProductTypes.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

