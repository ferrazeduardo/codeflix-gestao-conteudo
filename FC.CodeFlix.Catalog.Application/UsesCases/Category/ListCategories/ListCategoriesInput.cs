using FC.CodeFlix.Catalog.Application.Common;
using FC.CodeFlix.Catalog.Domain.SeedWork;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.ListCategories;

public class ListCategoriesInput
    : PaginatedListInput, IRequest<ListCategoriesOutput>
{
    public ListCategoriesInput(
        int page = 1, 
        int perPage = 15, 
        string search = "",
        string sort = "",
        SearchOrder dir = SearchOrder.Asc
       ) 
        : base(page, perPage, search, sort, dir)
    {
    }
}