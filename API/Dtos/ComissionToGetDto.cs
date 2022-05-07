using System;
using System.Collections.Generic;

namespace MoveMoney.API.Dtos
{
    public class ComissionToGetDto
    {
        public int SenderId { get; set; }
        public int RecipientId { get; set; }
        public double Amount { get; set; }
    }
}