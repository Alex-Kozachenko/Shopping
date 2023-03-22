namespace Shopping.Domain.Services.ProductFeaturesExtractor.Tests.Names;

internal readonly record struct Sample(string Line, params string[] Nouns);
internal static class Samples
{
    public static readonly Sample[] Vegetables = new Sample[]
    {
        new("Баклажан (импорт)", 
            "Баклажан"),
        new("Гриб шампиньон", 
            "Гриб"),
        new("Капуста пекинская", 
            "Капуста"),
        new("Картофель ( соц.товар), кг", 
            "Картофель"),
        new("Картофель (розовый )", 
            "Картофель"),
        new("Картофель белый", 
            "Картофель"),
        new("Картофель ранний", 
            "Картофель"),
        new("Корень имбиря", 
            "Корень имбиря"),
        new("Кукуруза вареная , 500 гр ( 2шт в уп.)", 
            "Кукуруза"),
        new("Лук репчатый (соц.товар) ,кг", 
            "Лук"),
        new("Перец сладкий красный", 
            "Перец"),
        new("Помидор на ветке", 
            "Помидор"),
        new("Редька Дайкон", 
            "Редька"),
        new("Салат Айсберг",
            "Салат"),
        new("Салат Микс , 150гр ( шт)",
            "Салат"),
        new("Сельдерей стеблевой",
            "Сельдерей"),
        new("Тыква (гитара)", 
            "Тыква"),
    };
}