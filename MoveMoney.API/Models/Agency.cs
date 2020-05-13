namespace MoveMoney.API.Models
{
    public class Agency
    {
        public int Id { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public string AgencyName { get; set; }
        public string City { get; set; }
        public decimal MoneyStored { get; set; }
    }
}