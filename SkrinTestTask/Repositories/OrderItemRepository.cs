using SkrinTestTask.Model.Entities;
using SkrinTestTask.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkrinTestTask.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        public void CreateOrderItem(OrderItem orderItem)
        {
            using (var db = new ApplicationContext())
            {
                db.OrderItems.Add(orderItem);
                db.SaveChanges();
            }
        }

        public OrderItem GetOrderItemByOrderId(int orderId)
        {
            using (var db = new ApplicationContext())
            {
                var orderItem = db.OrderItems.FirstOrDefault(oi => oi.Order.Id == orderId);
                if (orderItem is null)
                    throw new Exception($"There is no order item with orderId = \"{orderId}\" in the database");
                return orderItem;
            }
        }

        public OrderItem GetOrderItemByProductId(int productId)
        {
            using (var db = new ApplicationContext())
            {
                var orderItem = db.OrderItems.FirstOrDefault(oi => oi.Product.Id == productId);
                if (orderItem is null)
                    throw new Exception($"There is no order item with productId = \"{productId}\" in the database");
                return orderItem;
            }
        }
    }
}
