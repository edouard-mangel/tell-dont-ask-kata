using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.Repository;

namespace TellDontAskKata.Main.UseCase;

public class OrderApprovalUseCase(IOrderRepository orderRepository)
{
    public void Run(OrderApprovalRequest request)
    {
        CreatedOrder order = (CreatedOrder)orderRepository.GetById(request.OrderId);
        
        if (request.Approved) 
            orderRepository.Save(order.Approve());
        else
            orderRepository.Save(order.Reject());        
    }
}