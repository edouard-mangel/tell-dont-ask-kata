using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;

namespace TellDontAskKata.Main.UseCase;

public class OrderCreationUseCase(
    IOrderRepository orderRepository,
    IProductCatalog productCatalog)
{
    public void Run(SellItemsRequest request)
    {
        var orderItems = new List<OrderItem>();
        foreach (var itemRequest in request.Requests)
        {
            var product = productCatalog.GetByName(itemRequest.ProductName)
                          ?? throw new UnknownProductException();
            orderItems.Add(new OrderItem(product, itemRequest.Quantity));
        }
        var order = new Order(orderItems);
        orderRepository.Save(order);
    }
}