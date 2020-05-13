namespace MoveMoney.API.Models
{
    public class ClosingCashAgentDetail
    {
        public int Id { get; set; }
        public ClosingCashAgent ClosingCashAgentMaster { get; set; }
        public int ClosingCashAgentMasterId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}