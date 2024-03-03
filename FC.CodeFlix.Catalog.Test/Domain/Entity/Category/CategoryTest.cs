using FC.CodeFlix.Catalog.Domain.Exceptions;
using FluentAssertions;
using DomainEntity = FC.CodeFlix.Catalog.Domain.Entity;

namespace FC.CodeFlix.Catalog.Test.Domain.Entity.Category;

[Collection(nameof(CategoryTestFixture))]
public class CategoryTest
{

    public readonly CategoryTestFixture _categoryTestFixture;

    public CategoryTest(CategoryTestFixture categoryTestFixture)
    {
        _categoryTestFixture = categoryTestFixture;
    }
    
    [Fact(DisplayName = nameof(Instantiate))]
    [Trait("Domain", "Cateogry - Aggregates")]
    public void Instantiate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        var datetimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description);
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
        (category.IsActive).Should().BeTrue();
    }

    [Theory(DisplayName = nameof(InstantiateWithIsActive))]
    [Trait("Domain", "Cateogry - Aggregates")]
    [InlineData(true)]
    [InlineData(false)]
    public void InstantiateWithIsActive(bool IsActive)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        var datetimeBefore = DateTime.Now;
        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, IsActive);
        var datetimeAfter = DateTime.Now.AddSeconds(1);

        category.Should().NotBeNull();
        category.Name.Should().Be(validCategory.Name);
        category.Description.Should().Be(validCategory.Description);
        category.Id.Should().NotBeEmpty();
        category.CreatedAt.Should().NotBeSameDateAs(default(DateTime));
        (category.CreatedAt >= datetimeBefore).Should().BeTrue();
        (category.CreatedAt <= datetimeAfter).Should().BeTrue();
        (category.IsActive).Should().Be(IsActive);
    }

    [Theory(DisplayName = nameof(InstantiateErroWHenNameIsEmpity))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void InstantiateErroWHenNameIsEmpity(string? name)
    {
        
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action =
            () => new DomainEntity.Category(name!, validCategory.Description);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be empty or null", exception.Message);
    }

    [Fact(DisplayName = nameof(InstantiateErroWHenDescriptionIsEmpity))]
    [Trait("Domain", "Category - Aggregates")]
    public void InstantiateErroWHenDescriptionIsEmpity()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action =
            () => new DomainEntity.Category(validCategory.Name, null!);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should not be empty or null", exception.Message);
    }

    //nome deve ter no minimo 3 caracteres
    [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLesssThan3Characters))]
    [Trait("domain", "category - aggregates")]
    [MemberData(nameof(GetNameWithLessThan3Characters))]
    public void InstantiateErrorWhenNameIsLesssThan3Characters(string invalidName)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        
        Action action =
            () => new DomainEntity.Category(invalidName, validCategory.Description);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be at least 3 characters long", exception.Message);
    }
    
    public static IEnumerable<object[]> GetNameWithLessThan3Characters()
    {
        yield return new object[] { "1" };
        yield return new object[] { "12" };
        yield return new object[] { "a" };
        yield return new object[] { "ca" };
        yield return new object[] { "ux" };
    }


    //nome deve ter no maximo 255 carateres
    [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("domain", "category - aggregates")]
    public void InstantiateErrorWhenNameIsGreaterThan255Characters()
    {
        var invalidName = string.Join(null, Enumerable.Range(0, 257).Select(_ => "a").ToArray());

        Action action =
            () => new DomainEntity.Category(invalidName, "categoria valida");

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }

    //descricao deve ter no maximo 10000 caracteres
    [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10000Characters))]
    [Trait("domain", "category - aggregates")]
    public void InstantiateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var invalidDesccription = string.Join(null, Enumerable.Range(0, 10001).Select(_ => "a").ToArray());

        Action action =
            () => new DomainEntity.Category(validCategory.Name, invalidDesccription);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should be less or equal 10000 characters long", exception.Message);
    }
    

    [Fact(DisplayName = nameof(Activate))]
    [Trait("Domain", "Cateogry - Aggregates")]
    public void Activate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, false);
        category.Activate();

        Assert.True(category.IsActive);
    }

    [Fact(DisplayName = nameof(DesActivate))]
    [Trait("Domain", "Cateogry - Aggregates")]
    public void DesActivate()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();

        var category = new DomainEntity.Category(validCategory.Name, validCategory.Description, true);
        category.Desactivate();

        Assert.False(category.IsActive);
    }

    [Fact(DisplayName = nameof(Update))]
    [Trait("Domain", "Cateogry - Aggregates")]
    public void Update()
    {
        
        
        var category = _categoryTestFixture.GetValidCategory();
        var newValues = _categoryTestFixture.GetValidCategory();

        category.Update(newValues.Name, newValues.Description);
        
        Assert.Equal(newValues.Name, category.Name);
        Assert.Equal(newValues.Description,category.Description);
    }
    
    
    [Fact(DisplayName = nameof(UpdateOnlyName))]
    [Trait("Domain", "Cateogry - Aggregates")]
    public void UpdateOnlyName()
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        var newName = _categoryTestFixture.GetValidCategoryName();
        var currentDescription = validCategory.Description;
        
        validCategory.Update(newName);
        
        Assert.Equal(newName, validCategory.Name);
        Assert.Equal(currentDescription,validCategory.Description);
    }
    
    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
    [Trait("Domain", "Category - Aggregates")]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("    ")]
    public void UpdateErrorWhenNameIsEmpty(string? name)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action =
            () => validCategory.Update(name!);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should not be empty or null", exception.Message);
    }
    
    [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLesssThan3Characters))]
    [Trait("domain", "category - aggregates")]
    [InlineData("1")]
    [InlineData("12")]
    [InlineData("a")]
    [InlineData("ca")]
    public void UpdateErrorWhenNameIsLesssThan3Characters(string invalidName)
    {
        var validCategory = _categoryTestFixture.GetValidCategory();
        Action action =
            () => validCategory.Update(invalidName);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be at least 3 characters long", exception.Message);
    }

  
    [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
    [Trait("domain", "category - aggregates")]
    public void UpdateErrorWhenNameIsGreaterThan255Characters()
    {
        var invalidName = _categoryTestFixture.Faker.Lorem.Letter(256);
        var category = _categoryTestFixture.GetValidCategory();
        Action action =
            () => category.Update(invalidName);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
    }
    
    [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10000Characters))]
    [Trait("domain", "category - aggregates")]
    public void UpdateErrorWhenDescriptionIsGreaterThan10000Characters()
    {
        var category = _categoryTestFixture.GetValidCategory();
        var invalidDesccription = _categoryTestFixture.Faker.Commerce.ProductDescription();
        while(invalidDesccription.Length <= 10000)
        {
            invalidDesccription = $"{invalidDesccription} {_categoryTestFixture.Faker.Commerce.ProductDescription()}";
        }
        
        Action action =
            () => category.Update("nome new valido", invalidDesccription);

        var exception = Assert.Throws<EntityValidationException>(action);
        Assert.Equal("Description should be less or equal 10000 characters long", exception.Message);
    }
}