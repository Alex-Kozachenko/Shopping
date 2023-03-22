using Shopping.Contracts.Data;
using Shopping.Contracts.Data.Scoping;
using Shopping.Domain.Services.ProductFeaturesExtractor;

namespace Shopping.Domain.Services.ProductFeaturesExtractor.Tests;

internal class ProductServiceTests
{
    private const string defaultCategory = "Misc";

    [TestCase("Молоко вкусное 1л", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1л.", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1 л", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1 л.", ExpectedResult = 1000)]
    [TestCase("Молоко вкусное 1.1л.", ExpectedResult = 1100)]
    [TestCase("Молоко вкусное 0.1л", ExpectedResult = 100)]
    [TestCase("Молоко 0.1л вкусное", ExpectedResult = 100)]
    [TestCase("0.1л вкусное молоко", ExpectedResult = 100)]
    public long? Process_ShouldReturn_MassGramms(string line)
    {
        var product = Process(line);
        var wrapper = new CapacityFeatureScope(product.Features);
        return wrapper.MassGramms;
    }

    [TestCase("Яйцо стальное фирмы Чак Норрис 12шт", ExpectedResult = 12)]
    public long Process_ShouldReturn_Pieces(string line)
    {
        var product = Process(line);
        var wrapper = new CapacityFeatureScope(product.Features);
        return wrapper.Pieces;
    }

    [TestCase("Paclan Стакан пластиковый прозрачный", ExpectedResult = "Стакан")]
    [TestCase("Горох колотый желтый Донель 800г", ExpectedResult = "Горох")]
    [TestCase("Капуста", ExpectedResult = "Капуста")]
    [TestCase("Крупа гречневая ядрица 900г ТМ", ExpectedResult = "Крупа гречневая")]
    [TestCase("Крупа манная Крупиночка 0,9кг", ExpectedResult = "Крупа манная")]
    [TestCase("Крупа перловая Крупинка 800г", ExpectedResult = "Крупа перловая")]
    [TestCase("Крупа пшено 0,9 кг ТМ Крупиночка", ExpectedResult = "Крупа пшено")]
    [TestCase("Масло сливочное Золото Полесья 82,5%", ExpectedResult = "Масло сливочное")]
    [TestCase("Масло сливочное ТМ Милкавита 82,5% 180г", ExpectedResult = "Масло сливочное")]
    [TestCase("Мёд Липовый Берестов А.С. 240г", ExpectedResult = "Мёд")]
    [TestCase("Молоко вкусное аж глаза на лоб 1л", ExpectedResult = "Молоко")]
    [TestCase("Облепиха LiViAnTa, замороженная, 250г", ExpectedResult = "Облепиха")]
    [TestCase("Пена для бритья для нормальной кожи", ExpectedResult = "Пена для бритья")]
    [TestCase("Перец черный молотый МакМай 40г", ExpectedResult = "Перец")]
    [TestCase("Печень цыпленка-бройлера Благояр 900г", ExpectedResult = "Печень куриная")]
    [TestCase("Рис длиннозерный шлифованный 0,9кг ТМ", ExpectedResult = "Рис")]
    [TestCase("Сахар белый кристаллический 900г", ExpectedResult = "Сахар")]
    [TestCase("Тушка цыпленка-бройлера замороженная", ExpectedResult = "Тушка куриная")]
    [TestCase("Хлопья овсяные Крупиночка 450г", ExpectedResult = "Хлопья овсяные")]
    [TestCase("Яйцо стальное фирмы Чак Норрис 12шт", ExpectedResult = "Яйцо")]
    public string Process_ShouldReturn_GroupingName(string line)
    {
        var product = Process(line);
        var wrapper = new NamingFeatureScope(product.Features);
        return wrapper.GroupingName!;
    }

    private static Product Process(string info)
        => new ProductFeaturesExtractorService()
            .Process(new() { Info = info,  Category = defaultCategory });
}