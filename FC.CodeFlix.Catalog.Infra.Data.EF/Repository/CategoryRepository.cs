using FC.CodeFlix.Catalog.Application.Exceptions;
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

    public async Task<Category> Get(Guid Id, CancellationToken cancellationToken)
    {
        var category = await _categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);

        NotFoundException.ThrowIfNull(category, $"Category '{Id}' not found");

        return category!;
    }

    public Task Delete(Category aggregate, CancellationToken _)
    {
        return Task.FromResult(_categories.Remove(aggregate));
    }

    public Task Update(Category aggregate, CancellationToken _)
    {
        return Task.FromResult(_categories.Update(aggregate));
    }

    public async Task<SearchOutput<Category>> Search(SearchInput input, CancellationToken cancellationToken)
    {
        var total = await _categories.CountAsync();
        var items = await _categories.ToListAsync();
        return new(input.Page, input.PerPage, total, items);
    }
}