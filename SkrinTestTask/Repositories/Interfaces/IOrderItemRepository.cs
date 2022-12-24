using SkrinTestTask.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkrinTestTask.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        public void SaveOrderItem(OrderItem orderItem);
        public OrderItem GetOrderItemByOrderId(int orderId);
        public OrderItem GetOrderItemByProductId(int productId);
    }
}
