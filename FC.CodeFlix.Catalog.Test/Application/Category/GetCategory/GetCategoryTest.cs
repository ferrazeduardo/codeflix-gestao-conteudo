using FC.CodeFlix.Catalog.Application.Exceptions;
using FluentAssertions;
using Moq;
using UseCase = FC.CodeFlix.Catalog.Application.UsesCases.Category.GetCategory;

namespace FC.CodeFlix.Catalog.Test.Application.Category.GetCategory
{
    [Collection(nameof(GetCategoryTestFixture))]
    public class GetCategoryTest
    {
        private readonly GetCategoryTestFixture _fixture;

        public GetCategoryTest(GetCategoryTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = nameof(GetCategory))]
        [Trait("Application", "GetCategory - Use Cases")]
        public async Task GetCategory()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var exempleCategory = _fixture.GetValidCategory();
            repositoryMock.Setup(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(exempleCategory);
            var input = new UseCase.GetCategoryInput(exempleCategory.Id);
            var useCase = new UseCase.GetCategory(repositoryMock.Object);

            var output = await useCase.Handle(input, CancellationToken.None);

            repositoryMock.Verify(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);


            output.Should().NotBeNull();
            output.Name.Should().Be(exempleCategory.Name);
            output.Description.Should().Be(exempleCategory.Description);
            output.IsActived.Should().Be(exempleCategory.IsActive);
            output.CreatedAt.Should().Be(exempleCategory.CreatedAt);
        }


        [Fact(DisplayName = nameof(NotFoundExceptionWhenCategorydoesntExist))]
        [Trait("Application", "GetCategory - Use Cases")]
        public async Task NotFoundExceptionWhenCategorydoesntExist()
        {
            var repositoryMock = _fixture.GetRepositoryMock();
            var exempleGuid = Guid.NewGuid();
            repositoryMock.Setup(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new NotFoundException($"Category '{exempleGuid}' not found"));
            var input = new UseCase.GetCategoryInput(exempleGuid);
            var useCase = new UseCase.GetCategory(repositoryMock.Object);

            var task = async () => await useCase.Handle(input, CancellationToken.None);

            await task.Should().ThrowAsync<NotFoundException>();
            repositoryMock.Verify(x => x.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}