namespace Domain.Entities
{
    public class ComissionRange
    {
        public int Id { get; set; }
        public Comission Comission { get; set; }
        public int ComissionId { get; set; }
        public double MinAmount { get; set; }
        public double MaxAmount { get; set; }
        public double Percentage { get; set; }
    }
}