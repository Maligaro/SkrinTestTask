using SkrinTestTask.Model.Entities;

namespace SkrinTestTask.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public void CreateOrderr(Order order);
        public User GetOrderById(int id);
    }
}
