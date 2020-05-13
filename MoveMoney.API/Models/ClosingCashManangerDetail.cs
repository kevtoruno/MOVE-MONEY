namespace MoveMoney.API.Models
{
    public class ClosingCashManangerDetail
    {
        public int Id { get; set; }
        public ClosingCashManager ClosingCashMaster { get; set; }
        public int ClosingCashMasterId { get; set; }
        public ClosingCashAgent ClosingCashAgent { get; set; }
        public int ClosingCashAgentId { get; set; }
    }
}