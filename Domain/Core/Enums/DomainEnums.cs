using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core.Enums
{
    public enum OrderStatus 
    {
        Processing = 0, 
        Processed = 1, 
        Ready = 2, 
        OnDelivery = 3, 
        Completed = 10
    }

    public enum OrderDeliveryType
    {
        Pickup = 0,
        Delivery = 1
    }
}
