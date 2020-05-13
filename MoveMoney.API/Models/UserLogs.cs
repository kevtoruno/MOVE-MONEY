using System;

namespace MoveMoney.API.Models
{
    public class UserLogs
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public string Event { get; set; }
    }
}