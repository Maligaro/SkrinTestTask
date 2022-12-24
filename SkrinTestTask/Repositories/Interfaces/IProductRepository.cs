using SkrinTestTask.Model.Entities;

namespace SkrinTestTask.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public void SaveProduct(Product product);
        public Product GetProductById(int id);
        public Product GetProductByName(string name);
    }
}
