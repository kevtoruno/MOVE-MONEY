using System;

namespace Application.Core.Dtos
{
    public class OrderForDetailDto
    {

        /*General Order Info*/
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int AgencyOriginId { get; set; }
        public string AgencyOriginName { get; set; }
        public int AgencyDestinationId { get; set; }
        public string AgencyDestinationName { get; set; }
        public DateTime Created { get; set; }
        public string DeliveryType { get; set; }
        public string status { get; set; }
        public decimal Amount { get; set; }
        public decimal Taxes { get; set; }
        public decimal Comission { get; set; }
        public decimal Total { get; set; }

        /*Sender Info*/

        public string SenderName { get; set; }
        public string SenderPhoneNumber { get; set; }
        public string SenderCountry { get; set; }
        public string SenderCity { get; set; }
        public string SenderTypeIdentification { get; set; }
        public string SenderIdentification { get; set; }
        public string SenderAddress { get; set; }

        /*Receiver Info*/

        public string RecipientName { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string RecipientCountry { get; set; }
        public string RecipientCity { get; set; }
        public string RecipientTypeIdentification { get; set; }
        public string RecipientIdentification { get; set; }
        public string RecipientAddress { get; set; }
    }
}