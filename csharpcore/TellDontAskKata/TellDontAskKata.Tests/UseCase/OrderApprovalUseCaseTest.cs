using System;
using System.Collections.Generic;

using TellDontAskKata.Main.Domain;
using TellDontAskKata.Main.UseCase;
using TellDontAskKata.Tests.Doubles;
using Xunit;

namespace TellDontAskKata.Tests.UseCase
{
    public class OrderApprovalUseCaseTest
    {
        private readonly TestOrderRepository _orderRepository;
        private readonly OrderApprovalUseCase _useCase;

        public OrderApprovalUseCaseTest()
        {
            _orderRepository = new TestOrderRepository();
            _useCase = new OrderApprovalUseCase(_orderRepository);
        }


        [Fact]
        public void ApprovedExistingOrder()
        {
            var initialOrder = new CreatedOrder(new List<OrderItem>());
            
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = true
            };

            _useCase.Run(request);

            var savedOrder = _orderRepository.GetSavedOrder();
            Assert.IsType<ApprovedOrder>(savedOrder);
        }

        [Fact]
        public void RejectedExistingOrder()
        {
            var initialOrder = new CreatedOrder(new List<OrderItem>());

            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = false
            };

            _useCase.Run(request);

            var savedOrder = _orderRepository.GetSavedOrder();
            Assert.IsType<RejectedOrder>(savedOrder);
        }


        [Fact]
        public void CannotApproveRejectedOrder()
        {
            var initialOrder = new CreatedOrder(new List<OrderItem>()).Reject();

            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = true
            };


            Action actionToTest = () => _useCase.Run(request);
      
            Assert.Throws<InvalidCastException>(actionToTest);
            Assert.Null(_orderRepository.GetSavedOrder());
        }

        [Fact]
        public void CannotRejectApprovedOrder()
        {
            var initialOrder = new CreatedOrder(new List<OrderItem>()).Approve();

            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = false
            };


            Action actionToTest = () => _useCase.Run(request);
            Assert.Null(_orderRepository.GetSavedOrder());
        }

        [Fact]
        public void ShippedOrdersCannotBeRejected()
        {
            var initialOrder = new CreatedOrder(new List<OrderItem>()).Approve().Ship();
            _orderRepository.AddOrder(initialOrder);

            var request = new OrderApprovalRequest
            {
                OrderId = 1,
                Approved = false
            };


            Action action = () => _useCase.Run(request);

            Assert.Throws<InvalidCastException>(action);

            Assert.Null(_orderRepository.GetSavedOrder());
        }

    }
}
