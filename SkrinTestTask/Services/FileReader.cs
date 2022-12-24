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

        private List<User> users = new List<User>();
        private List<Order> orders = new List<Order>();
        private List<Product> products = new List<Product>();
        private List<OrderItem> orderItems = new List<OrderItem>();

        public XmlFileReader(IUserRepository userRepository, IOrderRepository orderRepository, 
            IProductRepository productRepository, IOrderItemRepository orderItemRepository) 
        {
            this.userRepository = userRepository;
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public void ImportFromFile (string filePath)
        {
            var fileExtention = Path.GetExtension(filePath);
            if (!fileExtention.Equals(Extention, StringComparison.OrdinalIgnoreCase))
                throw new Exception($"Expected {Extention} file extention, got {fileExtention} instead. {filePath}");
            
            var rawXmlData = File.ReadAllText(filePath);
            var xml = new XmlDocument();
            xml.LoadXml(rawXmlData);

            var orders = xml.DocumentElement.GetElementsByTagName("order");
            InitializeDbObjects(orders);
            SaveToDb();
        }

        private void SaveToDb()
        {
            users.ForEach(userRepository.SaveUser);
            products.ForEach(productRepository.SaveProduct);
            orders.ForEach(orderRepository.SaveOrder);
            orderItems.ForEach(orderItemRepository.SaveOrderItem);
        }

        private void InitializeDbObjects(XmlNodeList ordersXml)
        {
            foreach (XmlElement orderXml in ordersXml)
            {
                var user = CreateUser(orderXml);

                var order = new Order();
                order.User = user;
                order.TotalPrice = decimal.Parse(orderXml["sum"].InnerText, CultureInfo.InvariantCulture);
                var orderDate = DateTime.ParseExact(orderXml["reg_date"].InnerText, "yyyy.MM.dd", new CultureInfo("ru-RU"));
                order.OrderDate = orderDate;
                //dummy data
                order.OrderDate = order.OrderDate.AddDays(7);
                orders.Add(order);

                foreach (XmlElement productXml in orderXml.GetElementsByTagName("product"))
                {
                    var orderItem = new OrderItem();
                    orderItems.Add(orderItem);
                    orderItem.Amount = int.Parse(productXml["quantity"].InnerText);
                    orderItem.Product = CraeteProduct(productXml);
                    orderItem.Order = order;

                }
            }

        }

        private User CreateUser(XmlElement orderXml)
        {
            var user = new User();
            user.Name = orderXml["user"]["fio"].InnerText;
            user.Email = orderXml["user"]["email"].InnerText;
            //dummy data
            user.PhoneNumber = "+7 (800) 123-45-67";
            user.HashedPassword = Enumerable.Range(0, 64).Select(c => 'a').ToArray().ToString();
            user.HashedPassword = Enumerable.Range(0, 20).Select(c => 'a').ToArray().ToString();
            users.Add(user);
            return user;
        }

        private Product CraeteProduct(XmlElement productXml)
        {
            var result = new Product();

            var name = productXml["name"].InnerText;
            var price = decimal.Parse(productXml["price"].InnerText, CultureInfo.InvariantCulture);

            result = products.FirstOrDefault(p => p.Name.Equals(name));
            if (result == null)
            {
                result = new Product();
                result.Name = name;
                result.Price = price;
                products.Add(result);
            }

            return result;
        }
    }
}
