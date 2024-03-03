using FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;
using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.CreateCategory;

public class CreateCategoryInput : IRequest<CategoryModelOutput>
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActived { get; set; }

    public CreateCategoryInput(string name, string? description = null, bool isActived = true)
    {
        Name = name;
        Description = description ?? "";
        IsActived = isActived;
    }
}