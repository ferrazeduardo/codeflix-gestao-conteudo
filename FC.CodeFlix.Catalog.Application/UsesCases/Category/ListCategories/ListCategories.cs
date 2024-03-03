using FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Interface.Repository;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.ListCategories;

public class ListCategories : IListCategories
{
    private readonly ICategoryRepository _categoryRepository;

    public ListCategories(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<ListCategoriesOutput> Handle(ListCategoriesInput request, CancellationToken cancellationToken)
    {
        var searchOutput = await _categoryRepository.Search(
            new(
                request.Page,
                request.PerPage,
                request.Search,
                request.Sort,
                request.Dir),
            cancellationToken
        );

        var output = new ListCategoriesOutput(
            searchOutput.CurrentPage,
            searchOutput.PerPage,
            searchOutput.Total,
            searchOutput.Items.Select(x => CategoryModelOutput.FromCategory(x)).ToList()
        );

        return output;
    }
}