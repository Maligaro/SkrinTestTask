using SkrinTestTask.Model.Entities;

namespace SkrinTestTask.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public void CreateProduct(Product product);
        public User GetProductById(int id);
    }
}
