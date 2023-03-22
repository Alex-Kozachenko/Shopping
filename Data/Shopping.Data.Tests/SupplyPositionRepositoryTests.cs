using NMoneys;
using Shopping.Contracts.Data;
using Shopping.Contracts.Data.Scoping;
using Shopping.Contracts.Data.Supply;
using Shopping.Contracts.Domain.Services;
using System.Text.Json.Nodes;

#pragma warning disable CS8618

namespace Shopping.Data.Tests;

[TestFixture]
internal class SupplyPositionRepositoryTests
{
    private IDatabaseService databaseService;

    [SetUp]
    public void Setup()
    {
        var stream = new MemoryStream();
        databaseService = new DatabaseService(stream);
    }

    [Test]
    public void Add_ShouldUpdate_Collection()
    {
        AssertCount(0);
        var supplyPosition = Create();
        using (var connection = databaseService.OpenDatabaseConnection()) 
        {
            connection.GetCollection<SupplyPosition>().Insert(supplyPosition);
        }

        AssertCount(1);
    }

    [Test]
    public void Update_ShouldUpdate_Collection()
    {
        AssertCount(0);

        var supplyPosition = Create();
        using (var connection = databaseService.OpenDatabaseConnection())
        {
            var id = connection.GetCollection<SupplyPosition>().Insert(supplyPosition);
            supplyPosition = supplyPosition with 
            { 
                Id = id
            };
        }

        var expectedProduct = new Product() { Info = "Updated_Info", Category = "Category" };
        var updatedSupplyPosition = Create() with
        {
            Id = supplyPosition.Id,
            Invoice = new(),
            Product = expectedProduct
        };

        using (var connection = databaseService.OpenDatabaseConnection())
        {
            connection.GetCollection<SupplyPosition>()
                .Update(updatedSupplyPosition);
        }

        using (var connection = databaseService.OpenDatabaseConnection())
        {
            var result = connection.GetCollection<SupplyPosition>()
                .FindById(supplyPosition.Id)
                .Product;

            Assert.That(result, Is.EqualTo(expectedProduct));
        }
    }

    [Test]
    public void Remove_ShouldUpdate_Collection()
    {
        AssertCount(0);

        var supplyPosition = Create();

        using (var connection = databaseService.OpenDatabaseConnection())
        {
            var id = connection.GetCollection<SupplyPosition>()
                .Insert(supplyPosition);

            supplyPosition = supplyPosition with
            {
                Id = id
            };
        }

        AssertCount(1);

        using (var connection = databaseService.OpenDatabaseConnection())
        {
            connection.GetCollection<SupplyPosition>()
                .Delete(supplyPosition.Id);
        }

        AssertCount(0);
    }

    private SupplyPosition Create()
    {
        var productFeatures = new Dictionary<string, JsonNode>();
        _ = new CapacityFeatureScope(productFeatures)
        {
            MassGramms = 100,
            Pieces = 10,
        };

        _ = new NamingFeatureScope(productFeatures)
        {
            GroupingName = "Tasty"
        };

        var supplyPosition = new SupplyPosition()
        {
            Invoice = new Invoice()
            {
                Date = DateOnly.FromDateTime(DateTime.Now.Date),
                Price = new Money(100),
                Quantity = 1,
                Vendor = Shopping.Contracts.Data.Vendor.Walk,
            },
            Product = new Shopping.Contracts.Data.Product()
            {
                Category = "foo",
                Info = "bar",
                Features = productFeatures
            },
        };

        return supplyPosition;
    }

    private void AssertCount(int count)
    {
        using var connection = databaseService.OpenDatabaseConnection();
        var actualCount = connection.GetCollection<SupplyPosition>().Count();
        Assert.That(actualCount, Is.EqualTo(count));
    }
}
