using System;

namespace Domain.Entities
{
    public class ClosingCashManager
    {
        public int Id { get; set; }
        public User Closer { get; set; } //The Manager doing the closing cash.
        public int CloserId { get; set; }
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }
        public DateTime Created { get; set; }
        public decimal TotalAmount { get; set; } //The sum all of amounts closed
        public decimal TotalComission { get; set; } //The sum of all comissions earned
        public decimal TotalTaxes { get; set; } //The sum of all Taxes.

    }
}