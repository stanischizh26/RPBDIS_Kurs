using Microsoft.EntityFrameworkCore;
using Similar_products.Domain.Entities;
using Similar_products.Domain.Abstractions;

namespace Similar_products.Infrastructure.Repositories;

public class EnterpriseRepository(AppDbContext dbContext) : IEnterpriseRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task Create(Enterprise entity) => await _dbContext.Enterprises.AddAsync(entity);

    public async Task<IEnumerable<Enterprise>> Get(bool trackChanges) =>
        await (!trackChanges 
            ? _dbContext.Enterprises.AsNoTracking() 
            : _dbContext.Enterprises).ToListAsync();

    public async Task<Enterprise?> GetById(Guid id, bool trackChanges) =>
        await (!trackChanges ?
            _dbContext.Enterprises.AsNoTracking() :
            _dbContext.Enterprises).SingleOrDefaultAsync(e => e.Id == id);

    public void Delete(Enterprise entity) => _dbContext.Enterprises.Remove(entity);

    public void Update(Enterprise entity) => _dbContext.Enterprises.Update(entity);

    public async Task SaveChanges() => await _dbContext.SaveChangesAsync();
}

