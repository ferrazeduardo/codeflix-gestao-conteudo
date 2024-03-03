using MediatR;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.DeleteCategory;

public class DeleteCategoryInput : IRequest
{
    public DeleteCategoryInput(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}