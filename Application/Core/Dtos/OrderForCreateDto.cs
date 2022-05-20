using System;
using System.Collections.Generic;

namespace Application.Core.Dtos
{
    public class OrderForCreateDto
    {
        public int UserId { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public int AgencyDestinationId { get; set; }
        public string DeliveryType { get; set; }
        public double Amount { get; set; }
        public double Comission { get; set; } //+ amount + comission
    }
}