using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.DeleteCategory;

public interface IDeleteCategory : IRequestHandler<DeleteCategoryInput>
{
    
}