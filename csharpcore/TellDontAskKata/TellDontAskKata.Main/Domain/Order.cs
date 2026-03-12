using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public class Order
    {
        public decimal Total { get; }
        public string Currency { get; }
        public IList<OrderItem> Items { get; }
        public decimal Tax { get; }
        public OrderStatus Status { get; set; }
        public int Id { get; set; }

        /// <summary>
        /// Test constructor only.
        /// </summary>
        public Order()
        {
        }

        public Order(List<OrderItem> orderItems)
        {
            Status = OrderStatus.Created;
            Items = orderItems;
            Currency = "EUR";
            Total = orderItems.Sum(x => x.TaxedAmount);
            Tax = orderItems.Sum(x => x.Tax);
        }

        public void Approve()
        {
            if (IsShipped()) throw new ShippedOrdersCannotBeChangedException();
            if (IsRejected()) throw new RejectedOrderCannotBeApprovedException();
            Status = OrderStatus.Approved;
        }

        public void Reject()
        {
            if (IsShipped()) throw new ShippedOrdersCannotBeChangedException();
            if (IsApproved()) throw new ApprovedOrderCannotBeRejectedException();
            Status = OrderStatus.Rejected;
        }

        private bool IsShipped() => Status == OrderStatus.Shipped;
        private bool IsApproved() => Status == OrderStatus.Approved;
        private bool IsRejected() => Status == OrderStatus.Rejected;

        public void AssertCanBeShipped()
        {
            if (Status == OrderStatus.Created || Status == OrderStatus.Rejected)
            {
                throw new OrderCannotBeShippedException();
            }

            if (Status == OrderStatus.Shipped)
            {
                throw new OrderCannotBeShippedTwiceException();
            }
        }

        public void Ship()
        {
            AssertCanBeShipped();
            Status = OrderStatus.Shipped;
        }
    }
}