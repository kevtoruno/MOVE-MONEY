using Domain.Core.Enums;
using System;

namespace Domain.Entities
{
    public class Order
    {   
        public int Id { get; set; }
        public Customer Sender { get; set; }
        public int SenderId { get; set; }
        public Customer Recipient { get; set; }
        public int RecipientId { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Agency AgencyDestination { get; set; }
        public int AgencyDestinationId { get; set; }
        public ClosingCashAgentDetail ClosingCashAgentDetail { get; set; }
        //public Int64 OrderId { get; set; }
        public DateTime Created { get; set; }
        public bool IsClosed { get; set; }
        public OrderDeliveryType DeliveryType { get; set; }
        public OrderStatus Status { get; private set; }
        public double Comission { get; private set; }
        public double Amount { get; set; }
        public double Taxes { get; set; }
        public double Total { get; set; }

        public Order()
        {
            Created = DateTime.Now;
            IsClosed = false;
        }

        public void InitializeProcessingOrder(double comission)
        {
            Status = OrderStatus.Processing;
            Comission = comission;   
            Taxes = Comission * 0.10;
            Total = Amount + Taxes + Comission;
        }

        public bool ProcessOrder()
        {
            bool orderProcessed = false;

            if (Status == OrderStatus.Processing)
            {
                orderProcessed = true;

                SetStatusBasedOnDeliveryType();
            }

            return orderProcessed;
        }

        private void SetStatusBasedOnDeliveryType()
        {
            if (DeliveryType == OrderDeliveryType.Pickup)
            {
                Status = OrderStatus.Ready;
            }
            else
            {
                Status = OrderStatus.Processed;
            }
        }
    }
}