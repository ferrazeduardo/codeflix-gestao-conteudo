using FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.UpdateCategory;

public interface IUpdateCategory
    : IRequestHandler<UpdateCategoryInput, CategoryModelOutput>
{
    
}