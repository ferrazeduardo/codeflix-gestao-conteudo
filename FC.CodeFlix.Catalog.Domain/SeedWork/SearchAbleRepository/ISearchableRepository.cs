namespace FC.CodeFlix.Catalog.Domain.SeedWork;

public interface ISearchableRepository<TAggregate>
    where TAggregate : AggregateRoot
{
    Task<SearchOutput<TAggregate>> Search(
        SearchInput input,
        CancellationToken cancellationToken
    );
}