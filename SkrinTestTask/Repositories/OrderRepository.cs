using SkrinTestTask.Model.Entities;
using SkrinTestTask.Repositories.Interfaces;

namespace SkrinTestTask.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public void SaveOrder(Order order)
        {
            using (var db = new ApplicationContext())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
        }

        public Order GetOrderById(int id)
        {
            using (var db = new ApplicationContext()) 
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == id);
                if (order is null)
                    throw new Exception($"There is no order with id = \"{id}\" in the database");
                return order;
            }
        }
    }
}
