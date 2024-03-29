using System;
using System.Collections.Generic;

namespace Application.Core.Dtos
{
    public class UserForDetailDto
    {
        public int Id { get; set; }
        public string Agency { get; set; }
        public int AgencyOriginId { get; set; }
        public int AgencyCountryId { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public decimal Money { get; set; }
    }
}