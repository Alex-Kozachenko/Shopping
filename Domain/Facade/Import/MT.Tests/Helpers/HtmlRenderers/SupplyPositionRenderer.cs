using Shopping.Contracts.Data;
using Shopping.Contracts.Data.Supply;

namespace Shopping.Domain.Facade.Import.MT.Tests.Helpers.HtmlRenderers;

internal class SupplyPositionRenderer
{
    public static string Render(SupplyPosition supplyPosition)
        => $$"""
            <div class="history-order-good">
                {{Render(supplyPosition.Product)}}
                {{Render(supplyPosition.Invoice)}}
            </div>
            """;

    public static string Render(IEnumerable<SupplyPosition> supplyPositions)
        => supplyPositions.Select(Render)
            .Aggregate((x, y) => x + Environment.NewLine + y);

    private static string Render(Product product)
        => $$"""
        <a href="www.example.com" class="main-link">
                <div class="good-img">
                    <img class="main_good-img" />
                </div>
                <div class="main-desc">
                    <p class="main-name">{{product.Category}}</p>
                    <!---->
                    <div class="main-text">
                        <p>
                            {{product.Info}}
                        </p>
                    </div>
                </div>
            </a>
        """;

    private static string Render(Invoice info)
    => $$"""
            <div class="main-quantity">
                <span class="item-count_label">Количество</span>
                <span class="item-count">{{info.Quantity}}</span>
            </div>
            <div class="main-price">
                <div class="price-name">Цена</div>
                <!---->
                <p class="price-num price-num_history">
                    {{info.Price.Amount}} ₽
                </p>
            </div>
            <div class="main-summa">
                <div class="price-name">Сумма</div>
                <div class="sum-icon">
                    <p class="price-num price-num_history">
                        {{info.TotalPrice.Amount}} ₽
                    </p>
                </div>
            </div>
        """;

}