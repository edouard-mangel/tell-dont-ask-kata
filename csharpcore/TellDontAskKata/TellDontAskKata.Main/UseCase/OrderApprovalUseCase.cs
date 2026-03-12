using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;

namespace TellDontAskKata.Main.UseCase;

public class OrderApprovalUseCase(IOrderRepository orderRepository)
{
    public void Run(OrderApprovalRequest request)
    {
        var order = orderRepository.GetById(request.OrderId);
        if (request.Approved) order.Approve();
        else order.Reject();
        orderRepository.Save(order);
    }
}