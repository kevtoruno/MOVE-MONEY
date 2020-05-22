using System;
using System.Collections.Generic;

namespace MoveMoney.API.Dtos
{
    public class OrderForReturnDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public int AgencyDestinationId { get; set; }
        public string DeliveryType { get; set; }
        public string status { get; set; }
        public decimal Comission { get; set; }
        public decimal Amount { get; set; }
        public decimal Taxes { get; set; }
        public decimal Total { get; set; }
    }
}