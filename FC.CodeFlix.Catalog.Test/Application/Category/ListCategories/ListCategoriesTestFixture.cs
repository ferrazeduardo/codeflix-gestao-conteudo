using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UsesCases.Category.ListCategories;
using FC.CodeFlix.Catalog.Domain.Interface.Repository;
using FC.CodeFlix.Catalog.Domain.SeedWork;
using FC.CodeFlix.Catalog.Test.Common;
using Moq;

namespace FC.CodeFlix.Catalog.Test.Application.Category.ListCategories;

[CollectionDefinition(nameof(ListCategoriesTestFixture))]
public class ListCategoriesTestFixtureCollection
    : ICollectionFixture<ListCategoriesTestFixture>
{
}

public class ListCategoriesTestFixture
    : BaseFixture
{
    public Mock<ICategoryRepository> GetRepositoryMock()
        => new();

    public string GetValidCategoryName()
    {
        var categoryName = "";
        while (categoryName.Length < 3)
            categoryName = Faker.Commerce.Categories(1)[0];

        if (categoryName.Length > 255)
            categoryName = categoryName[..255];

        return categoryName;
    }

    public string GetValidCategoryDescription()
    {
        var categoryDescription = Faker.Commerce.ProductDescription();
        if (categoryDescription.Length > 10000)
            categoryDescription = categoryDescription[..10000];

        return categoryDescription;
    }

    public Catalog.Domain.Entity.Category GetExampleCategory()
        => new(
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRandomBoolean()
        );

    public List<Catalog.Domain.Entity.Category> GetExempleCategoriesList(int length = 10)
    {
        var list = new List<Catalog.Domain.Entity.Category>();
        for (int i = 0; i < length; i++)
            list.Add(GetExampleCategory());

        return list;
    }

    public ListCategoriesInput GetExempleInput()
    {
        var random = new Random();
        return new ListCategoriesInput(search: Faker.Commerce.ProductName(),
            sort: Faker.Commerce.ProductName(),
            dir: random.Next(0,10) > 5 ? SearchOrder.Asc : SearchOrder.Desc, perPage: random.Next(15, 100), page: random.Next(1, 10));
    }
}