using FC.CodeFlix.Catalog.Domain.Interface.Repository;
using FC.CodeFlix.Catalog.Test.Application.Category.Common;
using Moq;

namespace FC.CodeFlix.Catalog.Test.Application.Category.GetCategory;

[CollectionDefinition(nameof(GetCategoryTestFixture))]
public class GetCateogryTestFixtureCollection :
    ICollectionFixture<GetCategoryTestFixture>
{
}

public class GetCategoryTestFixture : CategoryUseCasesBaseFixture
{
    public Catalog.Domain.Entity.Category GetValidCategory()
    {
        return new(GetValidCategoryName(),GetValidCategoryDescription());
    }
}