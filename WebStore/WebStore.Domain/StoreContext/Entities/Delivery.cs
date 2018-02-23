using System;
using WebStore.Domain.StoreContext.Enums;
using WebStore.Shared.Entities;

namespace WebStore.Domain.StoreContext.Entities
{
    public class Delivery : Entity
    {
        public Delivery(DateTime estimatedDeliveryDate)
        {
            CreateDate = DateTime.Now;
            EstimatedDeliveryDate = estimatedDeliveryDate;
            Status = EDeliveryStatus.Waiting;
        }
        public DateTime CreateDate { get; private set; }
        public DateTime EstimatedDeliveryDate { get; private set; }
        public EDeliveryStatus Status { get; private set; }

        public void Ship()
        {
            // If EstimatedDate in past, don't delivery
            Status = EDeliveryStatus.Shipped;
        }

        public void Cancel()
        {
            // If Status is delivered, don't cancel
            Status = EDeliveryStatus.Canceled;
        }
    }
}