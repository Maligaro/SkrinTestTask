using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using SkrinTestTask.Model.Entities;
using SkrinTestTask.Repositories.Interfaces;
using System.Globalization;
using System.Xml;

namespace SkrinTestTask.Services
{
    public class XmlFileReader : IFileReader
    {
        public string Extention { get; } = ".xml";

        private IUserRepository userRepository;
        private IOrderRepository orderRepository;
        private IProductRepository productRepository;
        private IOrderItemRepository orderItemRepository;

        public XmlFileReader(IUserRepository userRepository, IOrderRepository orderRepository, 
            IProductRepository productRepository, IOrderItemRepository orderItemRepository) 
        {
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public void ReadFromFile (string filePath)
        {
            var fileExtention = Path.GetExtension(filePath);
            if (!fileExtention.Equals(Extention, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"Expected {Extention} file extention, got {fileExtention} instead. {filePath}");
            
            var rawXmlData = File.ReadAllText(filePath);
            var xml = new XmlDocument();
            xml.LoadXml(rawXmlData);

            var orders = xml.DocumentElement.GetElementsByTagName("order");
            SaveOrdersToDb(orders);
        }

        private void SaveOrdersToDb(XmlNodeList ordersXml)
        {
            foreach (XmlElement orderXml in ordersXml)
            {
                var user = CreateUser(orderXml["user"]);
                var order = CraeteOrder(orderXml, user);
                foreach (XmlElement productXml in orderXml.GetElementsByTagName("product"))
                {
                    var product = CraeteProduct(productXml);
                    var amount = int.Parse(productXml["quantity"].InnerText);
                    CraeteOrderItem(order, product, amount);
                }
            }
        }

        private OrderItem CraeteOrderItem(Order order, Product product, int amount)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = product.Id,
                Amount = amount
            };
            orderItemRepository.SaveOrderItem(orderItem);
            return orderItem;
        }

        private Order CraeteOrder(XmlElement orderXml, User user)
        {
            var orderDate = DateTime.ParseExact(orderXml["reg_date"].InnerText, "yyyy.MM.dd", new CultureInfo("ru-RU")).ToUniversalTime();
            var order = new Order
            {
                UserId = user.Id,
                TotalPrice = decimal.Parse(orderXml["sum"].InnerText, CultureInfo.InvariantCulture),
                OrderDate = orderDate,
                ShippingDate = orderDate.AddDays(7).ToUniversalTime()
            };
            orderRepository.SaveOrder(order);
            return order;
        }

        private User CreateUser(XmlElement userXml)
        {
            User user;
            var email = userXml["email"].InnerText;
            try
            {
                user = userRepository.GetUserByEmail(email);
            }
            catch (Exception ex) 
            {
                user = new User
                {
                    Email = userXml["email"].InnerText,
                    Name = userXml["fio"].InnerText,
                    //dummy data
                    PhoneNumber = "+7 (800) 123-45-67",
                    HashedPassword = string.Concat(Enumerable.Repeat('a', 64)),
                    PasswordSalt = string.Concat(Enumerable.Repeat('a', 20))
                };
                userRepository.SaveUser(user);
            }
            return user;
        }

        private Product CraeteProduct(XmlElement productXml)
        {
            Product product;
            var name = productXml["name"].InnerText;
            try
            {
                product = productRepository.GetProductByName(name);
            }
            catch (Exception ex)
            {
                product = new Product
                {
                    Name = productXml["name"].InnerText,
                    Price = decimal.Parse(productXml["price"].InnerText, CultureInfo.InvariantCulture)
                };
                productRepository.SaveProduct(product);
            }
            return product;
        }
    }
}
