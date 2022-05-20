namespace Application.Core.Dtos
{
    public class CustomerToReturnDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Identification { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
    }
}