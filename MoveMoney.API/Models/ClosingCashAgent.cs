using System;

namespace MoveMoney.API.Models
{
    public class ClosingCashAgent
    {
        public int Id { get; set; }
        public User Sender { get; set; } //User doing the closing cash, must be of type "Agent"
        public int SenderId { get; set; }
        public User Recipient { get; set; } //User that's going to receive the closing cash, must be of type "Agent"
        public int RecipientId { get; set; }
        public ClosingCashManangerDetail ClosingCashManangerDetail { get; set; }
        public bool IsClosed { get; set; } //Has it been closed by a Manager?
        public DateTime Created { get; set; }
    }
}