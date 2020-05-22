using System;
using System.Collections.Generic;

namespace MoveMoney.API.Dtos
{
    public class OrderForReturnDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public string AgencyDestinationName { get; set; }
        public string DeliveryType { get; set; }
        public string status { get; set; }
        public decimal Comission { get; set; }
        public decimal Amount { get; set; }
        public decimal Taxes { get; set; }
        public decimal Total { get; set; }
    }
}