namespace MoveMoney.API.Models
{
    public class ComissionRange
    {
        public int Id { get; set; }
        public Comission ComissionMaster { get; set; }
        public int ComissionMasterId { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
        public double Percentage { get; set; }
    }
}