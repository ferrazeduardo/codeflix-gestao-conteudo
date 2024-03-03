using FluentValidation;

namespace FC.CodeFlix.Catalog.Application.UsesCases.Category.UpdateCategory;

public class UpdateCategoryInputValidator
    : AbstractValidator<UpdateCategoryInput>
{
    public UpdateCategoryInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}