using FC.CodeFlix.Catalog.Test.Application.Category.Common;
using FC.CodeFlix.Catalog.Test.Common;

namespace FC.CodeFlix.Catalog.Test.Application.Category.DeleteCategory;

[CollectionDefinition(nameof(DeleteCategoryTestFixture))]
public class DeleteCategoryTestFixtureCollection
    : ICollectionFixture<DeleteCategoryTestFixture>
{ }

public class DeleteCategoryTestFixture
    : CategoryUseCasesBaseFixture
{ }