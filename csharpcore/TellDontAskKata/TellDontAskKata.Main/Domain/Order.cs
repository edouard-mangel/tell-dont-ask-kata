using System.Collections.Generic;
using System.Linq;
using TellDontAskKata.Main.UseCase;

namespace TellDontAskKata.Main.Domain
{
    public abstract class Order
    {
        public decimal Total => Items.Sum(i => i.TaxedAmount);
        public string Currency => "EUR";
        public IList<OrderItem> Items { get; }
        public decimal Tax => Items.Sum(x => x.Tax);
        public uint Id { get; init; }

        /// <summary>
        /// Test constructor only.
        /// </summary>
        protected Order(IEnumerable<OrderItem> orderItems)
        {
            Items = orderItems.ToList();
            Id = 1;
        }

        protected Order(Order other) { 
            Items = other.Items;
            Id = other.Id;
        }
    }

    public sealed class CreatedOrder : Order
    {
        public CreatedOrder(IEnumerable<OrderItem> orderItems):base (orderItems){}

        public ApprovedOrder Approve()
        {
            return new ApprovedOrder(this);
        }

        public RejectedOrder Reject()
        {
            return new RejectedOrder(this);
        }
    }

    public class RejectedOrder : Order
    {
        public RejectedOrder(CreatedOrder createdOrder):base(createdOrder){}

    }

    public class ApprovedOrder : Order
    {
        public ApprovedOrder(CreatedOrder createdOrder):base(createdOrder) {}

        public ShippedOrder Ship()
        {
            return new ShippedOrder(this);
        }
    }

    public class ShippedOrder: Order
    {
        public ShippedOrder(ApprovedOrder other) : base(other)
        {
        }
    }
}