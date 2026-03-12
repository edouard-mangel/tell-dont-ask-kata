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
        ApprovedOrder order = (ApprovedOrder)orderRepository.GetById(request.OrderId);
        
        shipmentService.Ship(order);
        order.Ship();
        orderRepository.Save(order);
    }
}