using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.ListCategories;

public interface IListCategories
: IRequestHandler<ListCategoriesInput,ListCategoriesOutput>
{
    
}