namespace Domain.Entities
{
    public class Agency
    {
        public int Id { get; set; }
        public Country? Country { get; set; }
        public int CountryId { get; set; }
        public string AgencyName { get; set; } = "";
        public string AgencyType { get; set; } // "HQ" or "regular" 
        public string City { get; set; } = "";
        public decimal MoneyStored { get; set; }
    }
}