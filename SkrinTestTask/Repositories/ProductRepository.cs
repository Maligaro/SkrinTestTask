using SkrinTestTask.Model.Entities;
using SkrinTestTask.Repositories.Interfaces;

namespace SkrinTestTask.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void CreateProduct(Product product)
        {
            using (var db = new ApplicationContext())
            {
                db.Products.Add(product);
                db.SaveChanges();
            }
        }

        public Product GetProductById(int id)
        {
            using (var db = new ApplicationContext())
            {
                var product = db.Products.FirstOrDefault(p => p.Id == id);
                if (product is null)
                    throw new Exception($"There is no product with id = \"{id}\" in the database");
                return product;
            }
        }
    }
}
