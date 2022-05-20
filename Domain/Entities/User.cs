using System;

namespace Domain.Entities
{
    public class User 
    {
        public int Id { get; set; }
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }
        public UserRole UserRole { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public decimal Money { get; set; }

        public void AddMoney(double amount)
        {
            Money += Convert.ToDecimal(amount);
        }
    }
}