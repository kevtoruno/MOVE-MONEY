using System;

namespace MoveMoney.API.Models
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
        public string DeliveryType { get; set; } //Pickup or Delivery
        public string Status { get; set; } //Processing, Processed, Ready, On Delivery, Completed
        public double Comission { get; set; }
        public double Amount { get; set; }
        public double Taxes { get; set; }
        public double Total { get; set; }

        public Order()
        {
            Created = DateTime.Now;
            IsClosed = false;
        }
    }
}