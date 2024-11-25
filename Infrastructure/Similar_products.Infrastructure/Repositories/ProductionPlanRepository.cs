using Microsoft.EntityFrameworkCore;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;

namespace Similar_products.Infrastructure.Repositories;

public class ProductionPlanRepository(AppDbContext dbContext) : IProductionPlanRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(ProductionPlan entity) => await _dbContext.ProductionPlans.AddAsync(entity);

    public async Task<IEnumerable<ProductionPlan>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.ProductionPlans.Include(e => e.Enterprise).Include(e => e.Product).AsNoTracking() 
            : _dbContext.ProductionPlans.Include(e => e.Enterprise).Include(e => e.Product)).ToListAsync();

    public async Task<ProductionPlan?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.ProductionPlans.Include(e => e.Enterprise).Include(e => e.Product).AsNoTracking() :
            _dbContext.ProductionPlans.Include(e => e.Enterprise).Include(e => e.Product)).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(ProductionPlan entity) => _dbContext.ProductionPlans.Remove(entity);

    public void Update(ProductionPlan entity) => _dbContext.ProductionPlans.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

