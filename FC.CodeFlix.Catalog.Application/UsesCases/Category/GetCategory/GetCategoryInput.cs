using FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.GetCategory;

public class GetCategoryInput : IRequest<CategoryModelOutput>
{
    public GetCategoryInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}