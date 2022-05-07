namespace MoveMoney.API.Dtos
{
    public class OrderForListDto
    {
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
        public int AgencyOriginId { get; set; }
        public string AgencyOriginName { get; set; }
        public int AgencyDestinationId { get; set; }
        public string AgencyDestinationName { get; set; }
        public string DeliveryType { get; set; }
        public string status { get; set; }
        public decimal Amount { get; set; }

    }
}