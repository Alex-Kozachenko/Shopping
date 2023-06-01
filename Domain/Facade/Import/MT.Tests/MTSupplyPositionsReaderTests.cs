using System.IO.Pipelines;
using Import.MT.Tests.Helpers;
using Shopping.Contracts.Data;
using Shopping.Contracts.Data.Supply;

namespace Shopping.Domain.Facade.Import.MT.Tests;

public class MTSupplyPositionsReaderTests
{
    [Test]
    public void Parse_SameProduct_InDifferentOrders_ShouldReturn_Duplicates()
    {
        var product = new Product
        {
            Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
            Category = "Пластиковая посуда",
        };

        var expectedPositions = new SupplyPosition[]
        {
            new() {
                Product = product,
                Invoice = new()
                {
                    Price = new NMoneys.Money(49),
                    //Url = "https://mt.delivery/single?id=206609",
                    Quantity = 1,
                    Date = new DateOnly(2020, 10, 1),
                }
            },
            new() {
                Product = product,
                Invoice = new()
                {
                    Price = new NMoneys.Money(51),
                    //Url = "https://mt.delivery/single?id=206609",
                    Quantity = 3,
                    Date = new DateOnly(2020, 11, 1),
                }
            },
        };

        var htmlStream = SupplyPackagesRenderer.Render(expectedPositions);
        var reader = new MTSupplyPositionsReader();
        var pipeReader = IOHelper.ToPipeReader(htmlStream);
        var result = reader.Read(pipeReader).Result;

        Assert.That(result, Is.EquivalentTo(expectedPositions));
    }

    [Test]
    public void Parse_SingleProduct_ShouldReturn_CorrectResult()
    {
        var expectedPosition = new SupplyPosition
        {
            Product = new()
            {
                Info = "Paclan Стакан пластиковый прозрачный Party Classic 200 мл 12шт",
                Category = "Пластиковая посуда",
            },
            Invoice = new Invoice()
            {
                Price = new NMoneys.Money(49),
                //Url = "https://mt.delivery/single?id=206609",
                Quantity = 1,
                Date = new DateOnly(2020, 10, 1),
            }
        };

        var htmlStream = SupplyPackagesRenderer.Render(expectedPosition);
        var pipeReader = IOHelper.ToPipeReader(htmlStream);
        var reader = new MTSupplyPositionsReader();
        var actualPosition = reader.Read(pipeReader).Result();

        Assert.Multiple(() =>
        {
            Assert.That(expectedPosition, Is.EqualTo(actualPosition));
        });
    }
}