using FC.CodeFlix.Catalog.Application.Interfaces;
using FC.CodeFlix.Catalog.Application.UsesCases.Category.CreateCategory;
using FC.CodeFlix.Catalog.Domain.Entity;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.Interface.Repository;
using FluentAssertions;
using Moq;
using useCases = FC.CodeFlix.Catalog.Application.UsesCases.Category.CreateCategory;

namespace FC.CodeFlix.Catalog.Test.Application.Category.CreateCategory;

[Collection(nameof(CreateCategoryTestFixture))]
public class CreateCategoryTest
{
    private readonly CreateCategoryTestFixture _fixture;

    public CreateCategoryTest(CreateCategoryTestFixture createCategoryTestFixture)
    {
        _fixture = createCategoryTestFixture;
    }

    [Fact(DisplayName = nameof(CreateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategory()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitWorkedMock = _fixture.GetUnitOfWorkMock();
        var useCase = new useCases.CreateCategory(repositoryMock.Object, unitWorkedMock.Object);

        var input = _fixture.GetInput();

        var output = await useCase.Handle(input, CancellationToken.None);
        //todo metodo assincorno recebe um cancelationtoken para que consigamos cancelar de fato aquele processo em algum momento
        //em todos os metodos asincrono, iremos passar sempre o cancelation token, que a gente consegue la no controller
        //pq assim quem solicitou a requisicao desistir ou fechar a conxao a gente nao precisa terminar todo o processo e so saber que desistiu na hora de responder pro controller
    //    repositoryMock.Verify(m => m.Insert(It.IsAny<Category>(), It.IsAny<CancellationToken>()), Times.Once());
        unitWorkedMock.Verify(m => m.Commit(It.IsAny<CancellationToken>()), Times.Once());

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActived.Should().Be(input.IsActived);
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyName))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategoryWithOnlyName()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitWorkedMock = _fixture.GetUnitOfWorkMock();
        var useCase = new useCases.CreateCategory(repositoryMock.Object, unitWorkedMock.Object);

        var input = new CreateCategoryInput(_fixture.GetValidCategoryName());

        var output = await useCase.Handle(input, CancellationToken.None);
        //todo metodo assincorno recebe um cancelationtoken para que consigamos cancelar de fato aquele processo em algum momento
        //em todos os metodos asincrono, iremos passar sempre o cancelation token, que a gente consegue la no controller
        //pq assim quem solicitou a requisicao desistir ou fechar a conxao a gente nao precisa terminar todo o processo e so saber que desistiu na hora de responder pro controller
        repositoryMock.Verify(m => m.Insert(It.IsAny<Catalog.Domain.Entity.Category>(), It.IsAny<CancellationToken>()), Times.Once());
        unitWorkedMock.Verify(m => m.Commit(It.IsAny<CancellationToken>()), Times.Once());

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be("");
        output.IsActived.Should().BeTrue();
    }

    [Fact(DisplayName = nameof(CreateCategoryWithOnlyNameAndDescription))]
    [Trait("Application", "CreateCategory - Use Cases")]
    public async void CreateCategoryWithOnlyNameAndDescription()
    {
        var repositoryMock = _fixture.GetRepositoryMock();
        var unitWorkedMock = _fixture.GetUnitOfWorkMock();
        var useCase = new useCases.CreateCategory(repositoryMock.Object, unitWorkedMock.Object);

        var input = new CreateCategoryInput(_fixture.GetValidCategoryName(), _fixture.GetValidCategoryDescription());

        var output = await useCase.Handle(input, CancellationToken.None);
        //todo metodo assincorno recebe um cancelationtoken para que consigamos cancelar de fato aquele processo em algum momento
        //em todos os metodos asincrono, iremos passar sempre o cancelation token, que a gente consegue la no controller
        //pq assim quem solicitou a requisicao desistir ou fechar a conxao a gente nao precisa terminar todo o processo e so saber que desistiu na hora de responder pro controller
        repositoryMock.Verify(m => m.Insert(It.IsAny<Catalog.Domain.Entity.Category>(), It.IsAny<CancellationToken>()), Times.Once());
        unitWorkedMock.Verify(m => m.Commit(It.IsAny<CancellationToken>()), Times.Once());

        output.Should().NotBeNull();
        output.Name.Should().Be(input.Name);
        output.Description.Should().Be(input.Description);
        output.IsActived.Should().BeTrue();
    }


    [Theory(DisplayName = nameof(ThrowWhenCantInstantiateCategory))]
    [Trait("Application", "CreateCategory - Use Cases")]
    [MemberData(
        nameof(CreateCategoryTestGenarator.GetInvalidInputs),
        parameters: 12,
        MemberType = typeof(CreateCategoryTestGenarator)
    )]
    public void ThrowWhenCantInstantiateCategory(CreateCategoryInput input, string exceptionMenssage)
    {
        var useCase =
            new useCases.CreateCategory(_fixture.GetRepositoryMock().Object, _fixture.GetUnitOfWorkMock().Object);

        Func<Task> task = async () => await useCase.Handle(input, CancellationToken.None);

        task.Should().ThrowAsync<EntityValidationException>()
            .WithMessage(exceptionMenssage);
    }
}