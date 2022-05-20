using System.Collections.Generic;

namespace Domain.Entities
{
    public class Comission
    {
        public int Id { get; set; }
        public Country CountrySender { get; set; }
        public int CountrySenderId { get; set; }
        public Country CountryReceiver { get; set; }
        public int CountryReceiverId { get; set; }
        public ICollection<ComissionRange> ComissionRange { get; set; }

        public double CalcComissionPerValue(double value)
        {
            double comissionValue = 0;

            var comission = ComissionRange
                .FirstOrDefault(cr => value >= cr.MinAmount && value <= cr.MaxAmount);

            if (comission != null)
                comissionValue = comission.Percentage * value;

            return comissionValue;
        }
    }
}