using System.Text;
using Shopping.Contracts.Data;
using Shopping.Contracts.Data.Supply;

internal class SupplyPackagesRenderer
{
    public static string Render(params SupplyPosition[] positions)
    {
        return string.Join('\n', positions
            .GroupBy(p => p.Invoice.Date)
            .Select(group => new
            {
                Positions = group,
                OrderDateStr = group.First().Invoice.Date.ToString("dd-MM-yyyy") + " 23:59"
            })
            .Select(x => RenderTable(x.OrderDateStr, x.Positions.ToArray())));
    }

    public static string RenderTable(string orderDateStr, params SupplyPosition[] positions)
        => $$"""
            <table>
                <tr class="history-order">
                    <td>
                        <div class="dropdownMenu">
                            {{RenderOrderInfo(orderDateStr)}}
                            <div class="dropdown opened">
                                <div id="dropdownContent">
                                    <div class="order-item-container">
                                        {{SupplyPositionRenderer.Render(positions)}}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        """;

    private static string RenderOrderInfo(string orderDateStr)
        => $$"""
            <div>
                <div class="order-head hopened">
                    <div class="order-data">
                        <div class="order-data_item id">123456</div>
                        <div class="order-data_item date">{{orderDateStr}} </div>
                        <div class="order-data_item sum">9999.00 ₽</div>
                        <div class="order-data_item state">оформлен</div>
                    </div>
                </div>
            </div>
            """;


}