using System.Text;
using Bogus;
using FC.CodeFlix.Catalog.Domain.Exceptions;
using FC.CodeFlix.Catalog.Domain.Validation;
using FluentAssertions;

namespace FC.CodeFlix.Catalog.Test.Domain.Validation;

public class DomainValidationTest
{
    public Faker Faker { get; set; } = new Faker();

    // nao ser null
    [Fact(DisplayName = nameof(NotNullOk))]
    [Trait("Domain", "DomainValidation - validation")]
    public void NotNullOk()
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        var value = Faker.Commerce.ProductName();
        Action action = () => DomainValidation.NotNull(value, fieldName);
        action.Should().NotThrow();
    }

    [Fact(DisplayName = nameof(NotNullThrowWhenNull))]
    [Trait("Domain", "DomainValidation - validation")]
    public void NotNullThrowWhenNull()
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        string? value = null;
        Action action = () => DomainValidation.NotNull(value, fieldName);
        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be empty or null");
    }

    //  nao ser null ou vazio
    [Theory(DisplayName = nameof(NotNUllOrEmptyThrowWhenEmpty))]
    [Trait("Domain", "DomainValidation - validation")]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
     public void NotNUllOrEmptyThrowWhenEmpty(string? target)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () =>
            DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should not be empty or null");
    }

     [Fact(DisplayName = nameof(NotNUllOrEmptyOk))]
     [Trait("Domain", "DomainValidation - validation")]
    public void NotNUllOrEmptyOk()
    {
        var target = Faker.Commerce.ProductName();
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        Action action = () =>
            DomainValidation.NotNullOrEmpty(target, fieldName);

        action.Should().NotThrow();
    }
    // tamanho minimo 
    [Theory(DisplayName = nameof(MinLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - validation")]
    [InlineData("ed",3)]
    public void MinLengthThrowWhenLess(string target,int minLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
      Action action = () =>  DomainValidation.MinLength(target, minLength, fieldName);
      action.Should().Throw<EntityValidationException>()
          .WithMessage($"{fieldName} should be at least {minLength} characters long");

    }
    // tamanho maximo

    [Theory(DisplayName = nameof(MaxLengthThrowWhenLess))]
    [Trait("Domain", "DomainValidation - validation")]
    [InlineData(255)]
    public void MaxLengthThrowWhenLess(int maxLength)
    {
        string fieldName = Faker.Commerce.ProductName().Replace(" ", "");
        int desiredLength = 256;
        StringBuilder randomString = new StringBuilder();

        Random random = new Random();
        string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        while (randomString.Length < desiredLength)
        {
            char randomChar = characters[random.Next(characters.Length)];
            randomString.Append(randomChar);
        }

        string generatedName = randomString.ToString();
        var target = randomString.ToString();
        Action action = () => DomainValidation.MaxLength(target, maxLength, fieldName);

        action.Should().Throw<EntityValidationException>()
            .WithMessage($"{fieldName} should be less or equal {maxLength} characters long");
    }
    
  
}