namespace MoveMoney.API.Dtos
{
    public class AgencyToReturnDto
    {
        public int Id { get; set; }
        public string AgencyName { get; set; }
        public string AgencyType { get; set; } // "HQ" or "regular" 
        public string City { get; set; }
        public decimal MoneyStored { get; set; }
    }
}