using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;
using TellDontAskKata.Main.Service;

namespace TellDontAskKata.Main.UseCase;

public class OrderShipmentUseCase(
    IOrderRepository orderRepository,
    IShipmentService shipmentService)
{
    public void Run(OrderShipmentRequest request)
    {
        var order = orderRepository.GetById(request.OrderId);
        order.AssertCanBeShipped();
        shipmentService.Ship(order);
        order.Ship();
        orderRepository.Save(order);
    }
}