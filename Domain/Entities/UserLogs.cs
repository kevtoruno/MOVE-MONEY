using System;

namespace Domain.Entities
{
    public class UserLogs
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Agency Agency { get; set; }
        public int AgencyId { get; set; }
        public DateTime Created { get; set; }
        public string EventType { get; set; }  //Transfer, Collected, Push, PushAgency, ClosingCashAgent, ClosingCashManager
        public string Description { get; set; }
        public decimal Amount { get; set; }

        public UserLogs()
        {
            Created = DateTime.Now;
        }
    }
}