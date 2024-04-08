using Microsoft.EntityFrameworkCore;

namespace Mission_11.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        private BookstoreContext context;

        public EFOrderRepository(BookstoreContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Order> Orders => context.Orders
                            .Include(o => o.Lines)
                            .ThenInclude(l => l.Book);

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Book));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}
