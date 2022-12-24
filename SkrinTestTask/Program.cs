using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Npgsql.Internal;
using SkrinTestTask.Model.Entities;
using SkrinTestTask.Repositories;
using SkrinTestTask.Services;

namespace SkrinTestTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var userRepository = new UserRepository();
            var orderRepository = new OrderRepository();
            var productRepository = new ProductRepository();
            var orderItemRepository = new OrderItemRepository();
            var reader = new XmlFileReader(userRepository, orderRepository, productRepository, orderItemRepository);
            reader.ReadFromFile(".\\DataFiles\\DataFile.xml");
        }
    }
}