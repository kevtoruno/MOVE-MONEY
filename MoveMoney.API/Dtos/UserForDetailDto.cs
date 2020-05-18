using System;
using System.Collections.Generic;

namespace MoveMoney.API.Dtos
{
    public class UserForDetailDto
    {
        public int Id { get; set; }
        public string Agency { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public decimal Money { get; set; }
    }
}