using Bogus;

namespace FC.CodeFlix.Catalog.IntegrationTest.Base;

public class BaseFixture
{
    public BaseFixture()
    {
        Faker = new Faker("pt_BR");
    }

    protected Faker Faker { get; set; }
    
    public bool GetRandomBoolean()
        => new Random().NextDouble() < 0.5;
}