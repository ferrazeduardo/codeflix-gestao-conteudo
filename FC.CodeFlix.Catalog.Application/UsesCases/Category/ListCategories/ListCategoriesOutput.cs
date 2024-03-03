using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.ListCategories;

public class ListCategoriesOutput
    : PaginatedListOutput<CategoryModelOutput>
{
    public ListCategoriesOutput(int page,
        int perPage,
        int total,
        IReadOnlyList<CategoryModelOutput> items) 
        : base(page, perPage, total, items)
    {
    }
}