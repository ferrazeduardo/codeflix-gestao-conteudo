using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.SeedWork;
using FC.CodeFlix.Catalog.Domain.Validation;

namespace FC.CodeFlix.Catalog.Domain.Entity;

public class Category : AggregateRoot
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }


    public Category(string name, string description, bool isActive = true)
        : base()
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        IsActive = isActive;
        CreatedAt = DateTime.Now;
        Validate();
    }

    private void Validate()
    {
        DomainValidation.NotNullOrEmpty(Name,nameof(Name));
        DomainValidation.MinLength(Name,3,nameof(Name));
        DomainValidation.MaxLength(Name,255,nameof(Name));
       
        DomainValidation.NotNull(Description,nameof(Description));
        DomainValidation.MaxLength(Description,10000,nameof(Description));
    }

    public void Activate()
    {
        IsActive = true;
        Validate();
    }

    public void Desactivate()
    {
        IsActive = false;
        Validate();
    }

    public void Update(string name, string? description = null)
    {
        Name = name;
        Description = description ?? Description;
        Validate();
    }
}