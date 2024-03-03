namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.Common;

public class CategoryModelOutput
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool IsActived { get; set; }
    public DateTime CreatedAt { get; set; }

    public CategoryModelOutput(Guid id  ,string name, string? description, bool isActived,DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description ?? "";
        IsActived = isActived;
        CreatedAt = createdAt;
    }

    public static CategoryModelOutput FromCategory(Domain.Entity.Category category)
        => new CategoryModelOutput(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.CreatedAt);
}