using MoveMoney.API.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace MoveMoney.API.Dtos
{
    public class UserForRegisterDto
    {
        public int Id { get; set; } 
        public int AgencyId { get; set; }
        public int UserRoleId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public decimal Money { get; set; }

        public UserForRegisterDto()
        {
            this.Created = DateTime.Now; 
            this.IsActive = true;  
            this.Money = 0;
        }
    }
}