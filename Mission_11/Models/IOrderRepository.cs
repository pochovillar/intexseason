﻿namespace Mission_11.Models
{
    public interface IOrderRepository
    {

        IQueryable<Order> Orders { get; }
        void SaveOrder(Order order);
    }
}
