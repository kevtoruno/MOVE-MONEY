using System;

namespace MoveMoney.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public TypeIdentification TypeIdentification { get; set; }
        public int TypeIdentificationID { get; set; }
        public string Identification { get; set; }
        public DateTime Created { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public Customer()
        {
            this.Created = DateTime.Now;
        }
    }
}