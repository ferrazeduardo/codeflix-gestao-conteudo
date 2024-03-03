using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.SeedWork;

namespace FC.CodeFlix.Catalog.Domain.Interface.Repository;

public interface ICategoryRepository 
    : IGenericRepository<Category>,
        ISearchableRepository<Category>
{
}