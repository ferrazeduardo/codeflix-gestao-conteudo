using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Interface.Repository;
using FC.CodeFlix.Catalog.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;

namespace FC.CodeFlix.Catalog.Infra.Data.EF.Repository;

public class CategoryRepository
    : ICategoryRepository
{
    private readonly CodeFlixCatalogDbContext _context;
    private DbSet<Category> _categories => _context.Set<Category>();

    public CategoryRepository(CodeFlixCatalogDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task Insert(Category aggregate, CancellationToken cancellationToken)
    {
        await _context.AddAsync(aggregate, cancellationToken);
    }

    public Task<Category> Get(Guid Id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Category aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Update(Category aggregate, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SearchOutput<Category>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}