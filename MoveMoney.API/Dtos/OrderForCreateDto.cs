using System;
using System.Collections.Generic;

namespace MoveMoney.API.Dtos
{
    public class OrderForCreateDto
    {
        public int UserId { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        
        public int AgencyDestinationId { get; set; }
    }
}