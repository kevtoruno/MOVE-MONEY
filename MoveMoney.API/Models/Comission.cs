using System.Collections.Generic;

namespace MoveMoney.API.Models
{
    public class Comission
    {
        public int Id { get; set; }
        public Country CountrySender { get; set; }
        public int CountrySenderId { get; set; }
        public Country CountryReceiver { get; set; }
        public int CountryReceiverId { get; set; }
        public ICollection<ComissionRange> ComissionRange { get; set; }
    }
}