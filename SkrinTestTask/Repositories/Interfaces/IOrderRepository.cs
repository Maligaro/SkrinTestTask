using SkrinTestTask.Model.Entities;

namespace SkrinTestTask.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public void CreateOrder(Order order);
        public Order GetOrderById(int id);
    }
}
