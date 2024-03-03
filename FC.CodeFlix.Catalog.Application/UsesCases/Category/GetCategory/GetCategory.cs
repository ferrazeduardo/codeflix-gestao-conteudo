using FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;
using FC.CodeFlix.Catalog.Domain.Interface.Repository;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.GetCategory;

public class GetCategory : IGetCategory
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategory(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }


    public async Task<CategoryModelOutput> Handle(GetCategoryInput request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.Get(request.Id,cancellationToken);
        return CategoryModelOutput.FromCategory(category);
    }
}